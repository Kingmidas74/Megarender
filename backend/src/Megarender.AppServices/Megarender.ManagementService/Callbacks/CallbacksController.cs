using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Megarender.Business.Modules.UserModule;
using Megarender.Domain;
using Megarender.Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Megarender.ManagementService.Callbacks
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class CallbacksController : ControllerBase
    {
        private readonly ISender _sender;
        
        public CallbacksController(ISender sender)
        {
            _sender = sender;
        }
        
        [HttpPost(nameof(Authentication))]
        public async Task<IActionResult> Authentication([FromBody]AuthenticationRequest request)
        {
            var user = await _sender.Send(new CreateUserCommand
            {
                Id = request.UserId,
                CommandId = Guid.NewGuid()
            });
            var privileges = user.UserAccessGroups
                .Where(ag => user.UserOrganizations.Select(uo => uo.Organization).Contains(ag.AccessGroup.Organization))
                .Select(agu => agu.AccessGroup.Privilege);
            
            
            

            return Ok(new Dictionary<string, string>
            {
                {$"{nameof(Domain.User)}.{nameof(Domain.User.Id)}", user.Id.ToString()},
                {$"{nameof(Domain.Privilege)}", privileges.ToString()}
            });
        }
    }
}