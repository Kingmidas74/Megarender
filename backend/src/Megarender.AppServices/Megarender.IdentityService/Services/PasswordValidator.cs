using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Megarender.IdentityService.CQRS;
using MediatR;

namespace Megarender.IdentityService
{
    public class PasswordValidator : IExtensionGrantValidator {

        private readonly ISender mediator;

        public PasswordValidator (ISender mediator) {
            this.mediator = mediator;
        }
        public string GrantType => "custom";

        public async Task ValidateAsync (ExtensionGrantValidationContext context) {
            var userPhone = context.Request.Raw.Get ("phone");
            var userPassword = context.Request.Raw.Get ("password");

            if (string.IsNullOrEmpty (userPhone) || string.IsNullOrEmpty (userPassword)) {
                context.Result = new GrantValidationResult (TokenRequestErrors.InvalidGrant);
                return;
            }

            var result = await this.mediator.Send(new FindUserByPhoneAndPasswordQuery{
                Password = userPassword,
                Phone = userPhone
            });
            if (result == null) {
                context.Result = new GrantValidationResult (TokenRequestErrors.InvalidGrant);
                return;
            }

            context.Result = new GrantValidationResult (userPhone, GrantType, new List<Claim> {
                new Claim ("userId", result.Id.ToString ()),
                new Claim ("userEmail", result.Email),
                new Claim ("userPhone", result.Phone),
                new Claim ("aud","megarender_api")
            });
            return;
        }
    }
}