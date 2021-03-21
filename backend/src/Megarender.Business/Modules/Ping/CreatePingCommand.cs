using MediatR;

namespace Megarender.Business.Modules.PingModule
{
    public class CreatePingCommand:IRequest<Pong>
    {
        public string Message { get; init; }  
    }
}