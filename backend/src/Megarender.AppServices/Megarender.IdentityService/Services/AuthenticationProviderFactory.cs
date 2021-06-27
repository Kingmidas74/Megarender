using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Megarender.IdentityService
{
    public class AuthenticationProviderFactory:IAuthenticationProviderFactory
    {
        private readonly AppDbContext _dbContext;
        private readonly IClientStore _clientStore;
        private readonly HttpClient _httpClient;
        
        public AuthenticationProviderFactory(AppDbContext dbContext, IClientStore clientStore, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _clientStore = clientStore;
            _httpClient = httpClient;
        }

        public async Task<IAuthenticationProvider> GetAuthenticationProviderByClient(Client client)
        {
            var providers = GetAllTypesThatImplementInterface<IAuthenticationProvider>();
            var currentProvider = client.AllowedGrantTypes.First() switch
            {
                "megarender" => providers.Single(p => p.IsAssignableFrom(typeof(MegarenderAuthenticationProvider))),
                _ => throw new ConstraintException()
            };
            return await Task.FromResult((IAuthenticationProvider)Activator.CreateInstance(currentProvider, client, _httpClient));
        }

        private IEnumerable<Type> GetAllTypesThatImplementInterface<T>() => Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface);
    }
}