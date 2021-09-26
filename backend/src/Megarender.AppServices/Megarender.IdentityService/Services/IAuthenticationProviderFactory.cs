using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Megarender.IdentityService
{
    public interface IAuthenticationProviderFactory
    {
        Task<IAuthenticationProvider> GetAuthenticationProviderByClient(Client client);
    }
}