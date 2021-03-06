using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Megarender.StorageService.CQRS
{
    public class UploadFileByUserHandler : IRequestHandler<UploadFileByUserCommand, Guid>
    {
        public UploadFileByUserHandler()
        {
        }

        public async Task<Guid> Handle(UploadFileByUserCommand request, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Guid.NewGuid());
        }
    }
}