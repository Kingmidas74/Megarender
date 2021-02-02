using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.BusinessServices.Modules;
using Megarender.MemoryStorage;

namespace Megarender.BusinessServices.PipelineBehaviors
{
    public class IdempotentBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IIdempotentRequest
    {
        private readonly ICommandStore _commandStore;

        public IdempotentBehaviour(ICommandStore commandStore)
        {
            _commandStore = commandStore ?? throw new ArgumentNullException(nameof(commandStore));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var exists = await _commandStore.Exists(request.CommandId);

            if (exists)
            {
                return default;
            }

            var response = await next();

            await _commandStore.Save(request.CommandId);

            return response;
        }
    }    
}