using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.Business.Modules;
using Megarender.DataAccess;
using Megarender.DataAccess.Extensions;
using Microsoft.Extensions.Logging;

namespace Megarender.Business.PipelineBehaviors
{

    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: ITransactionalRequest
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IAPIContext _dbContext;
        private readonly int _retryCount = 3;

        public TransactionBehaviour(IAPIContext dbContext, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            try
            {
                await _dbContext.RetryOnExceptionAsync(_retryCount, async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}", request);

                    using var transaction = await _dbContext.BeginTransactionAsync();

                    response = await next();

                    await transaction.CommitAsync();

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}", request);
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

                _dbContext.RollbackTransaction();

                _logger.LogError(e.Message, e.StackTrace);

                throw e;
            }
        }
    }
    
}