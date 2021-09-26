using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Megarender.IdentityService {
    public class ProfileService : IProfileService {
        public Task GetProfileDataAsync (ProfileDataRequestContext context) {
            context.IssuedClaims.AddRange (
                context.Subject.Claims.Select (x => new Claim (x.Type, x.Value))
            );
            return Task.CompletedTask;
        }

        public Task IsActiveAsync (IsActiveContext context) {
            context.IsActive = true;
            return Task.FromResult (true);
        }
    }
}