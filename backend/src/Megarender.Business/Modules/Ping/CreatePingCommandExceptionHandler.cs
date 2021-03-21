using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Megarender.Business.Modules.PingModule
{
    public class CreatePingCommandExceptionHandler : AsyncRequestExceptionHandler<CreatePingCommand, Pong>         
    {
        public CreatePingCommandExceptionHandler()
        {
        }

        protected override async Task Handle(CreatePingCommand request, Exception exception, RequestExceptionHandlerState<Pong> state, CancellationToken cancellationToken)
        {
            state.SetHandled(new Pong {
                Message = exception.Message
            });
            await Task.CompletedTask;
        }
    }
}