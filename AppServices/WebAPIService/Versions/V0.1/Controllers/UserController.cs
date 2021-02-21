using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Megarender.WebAPIService.Extensions;
using System.Threading.Tasks;
using MediatR;
using Megarender.BusinessServices.Modules.UserModule;
using System;
using WebAPIService;
using System.Threading;

namespace Megarender.WebAPIService.Versions.V01.Controllers {

    [Route ("api/{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(Constants.V01)]
    [Authorize]
    public class UsersController : ControllerBase {    

        private readonly ISender mediator;
            
        public UsersController (ISender mediator) {            
            this.mediator = mediator;
        }
        
        /// <summary>
        /// Get all users by organization
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(GetUsersByOrganizationQuery getUsersByOrganization, CancellationToken cancellationToken=default(CancellationToken)) {
            return Ok(await mediator.Send(getUsersByOrganization, cancellationToken));
        }

        /// <summary>
        /// Get info about one user by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> CetById(Guid Id, CancellationToken cancellationToken=default(CancellationToken)) {
            return Ok(await mediator.Send(new GetUserQuery{Id=Id}, cancellationToken));
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand createUserCommand, CancellationToken cancellationToken=default(CancellationToken))
        {
            var result = await mediator.Send(createUserCommand, cancellationToken);
            return Created($"{nameof(User)}/{result.Id}",result); 
        }

        /// <summary>
        /// Create new user in organization
        /// </summary>
        /// <param name="createAndAddUserToOrganizationCommand"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateAndAddToOrganization))]
        public async Task<IActionResult> CreateAndAddToOrganization([FromBody]CreateAndAddUserToOrganizationCommand createAndAddUserToOrganizationCommand, CancellationToken cancellationToken=default(CancellationToken))
        {
            var result = await mediator.Send(createAndAddUserToOrganizationCommand, cancellationToken);
            return Created($"{nameof(User)}/{result.Id}",result); 
        }
    }
}