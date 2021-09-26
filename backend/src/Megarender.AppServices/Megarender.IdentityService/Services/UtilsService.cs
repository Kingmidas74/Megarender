using System;
using System.Linq;
using IdentityServer4.Models;

namespace Megarender.IdentityService
{
    public sealed class UtilsService
    {
        private readonly AppDbContext _identityDbContext;
        public UtilsService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        internal string GenerateSalt(int length) {
            var rdm = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                                .Select(s => s[rdm.Next(s.Length)]).ToArray());
        }
        internal string GenerateCode(int min, int max)
        {
            var rdm = new Random();
            return rdm.Next(min, max).ToString();
            string code;

            do {
                code = rdm.Next(min, max).ToString();
            }
            while(_identityDbContext.Identities.Select(x=>x.Code).Contains(code));            
            return code;
        }

        internal string HashedPassword(string phone,string salt, string pepper) => $"{salt}{phone}{pepper}".Sha256();
    }
}