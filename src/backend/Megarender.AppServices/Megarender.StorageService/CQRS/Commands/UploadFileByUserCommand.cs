using System;
using MediatR;

namespace Megarender.StorageService.CQRS
{
    public class UploadFileByUserCommand:IRequest<Guid>
    {
        public Guid UserId {get;set;}
        public byte[] Content {get;set;}
    }
}