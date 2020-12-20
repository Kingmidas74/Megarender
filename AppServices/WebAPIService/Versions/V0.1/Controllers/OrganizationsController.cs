using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Megarender.WebAPIService.Extensions;
using System.Threading.Tasks;
using MediatR;
using Megarender.BusinessServices.Modules.UserModule;
using Megarender.Domain;
using System;
using WebAPIService;

namespace Megarender.WebAPIService.Versions.V01.Controllers {

    [Route ("api/{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> Get() {
            return Ok(await mediator.Send(new GetOrganizationsQuery()));
        }

        /// <summary>
        /// Get organization by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id) {
            return Ok(await mediator.Send(new GetOrganizationQuery{Id=Id}));
        }

        /// <summary>
        /// Create new organization
        /// </summary>
        /// <param name="createOrganizationCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody]CreateOrganizationCommand createOrganizationCommand) {
            var result = await mediator.Send(createOrganizationCommand);
            return Created($"{nameof(Organization)}/{result.Id}",result);
        }
    }
}