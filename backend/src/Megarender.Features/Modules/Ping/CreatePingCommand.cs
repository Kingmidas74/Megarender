using MediatR;

namespace Megarender.Features.Modules.PingModule
{
    public class CreatePingCommand:IRequest<Pong>
    {
        public string Message { get; init; }  
    }
}