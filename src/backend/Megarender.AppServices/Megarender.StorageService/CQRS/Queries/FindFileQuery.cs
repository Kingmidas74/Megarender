using System;
using MediatR;

namespace Megarender.StorageService.CQRS
{
    public class FindFileQuery:IRequest<byte[]>
    {
        public Guid UserId {get;set;}
        public Guid FileId {get;set;}
    }
}