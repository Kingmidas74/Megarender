using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Megarender.IdentityService
{
    public interface IAuthenticationProvider
    {
        Task<Dictionary<string,object>> SureUserExist(User user);
    }
}