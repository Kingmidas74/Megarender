using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.DataAccess;
using Megarender.DataAccess.Extensions;
using Megarender.Features.Modules;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Megarender.Features.PipelineBehaviors
{

    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: ITransactionalRequest
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IAPIContext _dbContext;
        private readonly int _retryCount = 3;

        public TransactionBehaviour(IAPIContext dbContext, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(null, nameof(dbContext));
            _logger = logger ?? throw new ArgumentException(null, nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;
            IDbContextTransaction transaction = default;

            try
            {
                await _dbContext.RetryOnExceptionAsync(_retryCount, async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}", request);

                    transaction ??= await _dbContext.BeginTransactionAsync(cancellationToken);
    
                    response = await next();

                    await _dbContext.CommitTransactionAsync(transaction, cancellationToken);

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}", request);
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

                _dbContext.RollbackTransaction(transaction);

                _logger.LogError(e.Message, e.StackTrace);

                throw;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
            }
        }
    }
    
}