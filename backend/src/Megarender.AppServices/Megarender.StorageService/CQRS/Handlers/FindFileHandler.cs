using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Megarender.StorageService.CQRS
{
    public class FindFileHandler : IRequestHandler<FindFileQuery, byte[]>
    {
        public FindFileHandler()
        {
        }

        public async Task<byte[]> Handle(FindFileQuery request, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new byte[256]);
        }
    }
}