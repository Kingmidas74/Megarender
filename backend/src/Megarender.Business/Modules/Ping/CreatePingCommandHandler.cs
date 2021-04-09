using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Megarender.Business.Modules.PingModule
{
    public class CreatePingCommandHandler : IRequestHandler<CreatePingCommand, Pong>
    {
        public CreatePingCommandHandler()
        {
        }
        public async Task<Pong> Handle(CreatePingCommand request, CancellationToken cancellationToken = default)
        {   
            if(request.Message.Equals("Error2"))
                throw new Exception("It's error ping");
            return await Task.FromResult<Pong>(new Pong {
                Message = $"It's ping with message {request.Message}",
                Timestamp = DateTime.UtcNow
            });
        }
    }
}