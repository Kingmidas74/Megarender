using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using Megarender.IdentityService.CQRS;

namespace Megarender.IdentityService
{
    public class PasswordValidator : IExtensionGrantValidator {

        private readonly ISender _mediator;

        public PasswordValidator (ISender mediator) {
            _mediator = mediator;
        }
        public string GrantType => "custom";

        public async Task ValidateAsync (ExtensionGrantValidationContext context) 
        {

            var result = await _mediator.Send(new FindUserByPhoneAndPasswordQuery{
                Password = context.Request.Raw.Get (nameof(FindUserByPhoneAndPasswordQuery.Password).ToLower(CultureInfo.InvariantCulture)),
                Phone = context.Request.Raw.Get (nameof(FindUserByPhoneAndPasswordQuery.Phone).ToLower(CultureInfo.InvariantCulture))
            });
            if (result == null) 
            {
                context.Result = new GrantValidationResult (TokenRequestErrors.InvalidGrant);
                return;
            }

            var communicationData = result.PreferredCommunicationChannel switch
            {
                CommunicationChannelId.Email => result.CommunicationChannelsData.Email,
                CommunicationChannelId.Telegram => result.CommunicationChannelsData.TelegramId,
                CommunicationChannelId.Phone => result.CommunicationChannelsData.PhoneNumber,
                _ => throw new ArgumentOutOfRangeException(nameof(User.PreferredCommunicationChannel))
            };

            context.Result = new GrantValidationResult (communicationData, GrantType, new List<Claim> {
                new(nameof(User.Id), result.Id.ToString ()),
                new(type:nameof(User.PreferredCommunicationChannel),result.PreferredCommunicationChannel.ToString()),
                new(nameof(CommunicationChannelsData), communicationData),
               // new(JwtRegisteredClaimNames.Aud, "megarender_api")
            });
        }
    }
}