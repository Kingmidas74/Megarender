using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Megarender.Business.Modules.OrganizationModule;
using Megarender.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using Megarender.WebServiceCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace Megarender.ManagementService.Versions.V01.Controllers {

    [Route ("[controller]")]
    [ApiController]
    [ApiVersion(Constants.V01)]
    [Authorize]
    public class OrganizationsController : ControllerBase {    

        private readonly ISender _mediator;
            
        public OrganizationsController (ISender mediator) {            
            this._mediator = mediator;
        }
        
        /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Organization>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken=default) {
            return Ok(await _mediator.Send(new GetOrganizationsQuery(), cancellationToken));
        }

        /// <summary>
        /// Get organization by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Organization))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken=default) {
            return Ok(await _mediator.Send(new GetOrganizationQuery{Id=id}, cancellationToken));
        }

        /// <summary>
        /// Create new organization
        /// </summary>
        /// <param name="createOrganizationCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Organization))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrganization([FromBody]CreateOrganizationCommand createOrganizationCommand, CancellationToken cancellationToken=default) {
            createOrganizationCommand.CreatedBy = User.ExtractIdentifier();
            var result = await _mediator.Send(createOrganizationCommand, cancellationToken);
            return Created($"{nameof(Organization)}/{result.Id}",result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DisableOrganization(
            [FromBody] DisableOrganizationCommand disableOrganizationCommand,
            CancellationToken cancellationToken = default)
        {
            disableOrganizationCommand.ModifyBy = User.ExtractIdentifier();
            await _mediator.Send(disableOrganizationCommand, cancellationToken);
            return NoContent();
        }
    }
}