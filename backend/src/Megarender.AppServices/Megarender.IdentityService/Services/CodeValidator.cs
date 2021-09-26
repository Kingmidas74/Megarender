using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using Megarender.Domain.Exceptions;
using Megarender.IdentityService.CQRS;

namespace Megarender.IdentityService
{
    public class CodeValidator : IExtensionGrantValidator {

        private readonly ISender _mediator;
        private readonly IAuthenticationProviderFactory _providerFactory;

        public CodeValidator (ISender mediator, IAuthenticationProviderFactory providerFactory)
        {
            _mediator = mediator;
            _providerFactory = providerFactory;
        }
        public string GrantType => "megarender";

        public async Task ValidateAsync (ExtensionGrantValidationContext context) 
        {

            var result = await _mediator.Send(new VerifyCodeCommand {
                Code = context.Request.Raw.Get (nameof(SureExistUserByPhoneAndCodeCommand.Code).ToLower(CultureInfo.InvariantCulture)),
                Phone = context.Request.Raw.Get (nameof(SureExistUserByPhoneAndCodeCommand.Phone).ToLower(CultureInfo.InvariantCulture))
            });
            if (result == null) 
            {
                context.Result = new GrantValidationResult (TokenRequestErrors.InvalidGrant);
                return;
            }

            var authenticationProvider = await _providerFactory.GetAuthenticationProviderByClient(context.Request.Client);
            var userData = await authenticationProvider.SureUserExist(result);

            if (userData?.Count<=0) throw new ConstraintException();
            var claims = userData?.Select(data =>
                new Claim(data.Key, data.Value?.ToString() ?? throw new NullException())) ?? throw new NullException();
            
            context.Result = new GrantValidationResult (context.Request.Client.ClientName, GrantType, claims);
        }
    }
}