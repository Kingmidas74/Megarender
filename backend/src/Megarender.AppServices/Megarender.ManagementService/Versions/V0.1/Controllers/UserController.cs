using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.Business.Modules.UserModule;
using Megarender.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Megarender.ManagementService.Versions.V01.Controllers {

    [Route ("[controller]")]
    [ApiController]
    [ApiVersion(Constants.V01)]
    [Authorize]
    public class UsersController : ControllerBase {    

        private readonly ISender _mediator;
            
        public UsersController (ISender mediator) {            
            _mediator = mediator;
        }
        
        /// <summary>
        /// Get all users by organization
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(GetUsersByOrganizationQuery getUsersByOrganization, CancellationToken cancellationToken=default) {
            return Ok(await _mediator.Send(getUsersByOrganization, cancellationToken));
        }

        /// <summary>
        /// Get info about one user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CetById(Guid id, CancellationToken cancellationToken=default) {
            return Ok(await _mediator.Send(new GetUserQuery{Id=id}, cancellationToken));
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand createUserCommand, CancellationToken cancellationToken=default)
        {
            var result = await _mediator.Send(createUserCommand, cancellationToken);
            return Created($"{nameof(User)}/{result.Id}",result); 
        }

        /// <summary>
        /// Create new user in organization
        /// </summary>
        /// <param name="createAndAddUserToOrganizationCommand"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateAndAddToOrganization))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAndAddToOrganization([FromBody]CreateAndAddUserToOrganizationCommand createAndAddUserToOrganizationCommand, CancellationToken cancellationToken=default)
        {
            var result = await _mediator.Send(createAndAddUserToOrganizationCommand, cancellationToken);
            return Created($"{nameof(User)}/{result.Id}",result); 
        }
    }
}