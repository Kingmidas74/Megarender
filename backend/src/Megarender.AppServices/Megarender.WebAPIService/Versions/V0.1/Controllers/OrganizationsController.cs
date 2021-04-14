using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Megarender.Business.Modules.UserModule;
using Megarender.Domain;
using System;
using System.Threading;

namespace Megarender.WebAPIService.Versions.V01.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    [ApiVersion(Constants.V01)]
    [Authorize]
    public class OrganizationsController : ControllerBase {    

        private readonly ISender mediator;
            
        public OrganizationsController (ISender mediator) {            
            this.mediator = mediator;
        }
        
        /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken=default) {
            return Ok(await mediator.Send(new GetOrganizationsQuery(), cancellationToken));
        }

        /// <summary>
        /// Get organization by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken=default) {
            return Ok(await mediator.Send(new GetOrganizationQuery{Id=Id}, cancellationToken));
        }

        /// <summary>
        /// Create new organization
        /// </summary>
        /// <param name="createOrganizationCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody]CreateOrganizationCommand createOrganizationCommand, CancellationToken cancellationToken=default) {
            var result = await mediator.Send(createOrganizationCommand, cancellationToken);
            return Created($"{nameof(Organization)}/{result.Id}",result);
        }
    }
}