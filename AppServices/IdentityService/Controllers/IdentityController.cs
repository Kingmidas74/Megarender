using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IdentityService.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{    
    [Route ("[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class IdentityController:ControllerBase
    {
        private readonly ISender Mediator;

        public IdentityController(ISender mediator)
        {
            this.Mediator = mediator;
        }   

        /// <summary>
        /// Create new identity
        /// </summary>
        /// <param name="createIdentityCommand"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateIdentity))]
        public async Task<IActionResult> CreateIdentity([FromBody]CreateIdentityCommand createIdentityCommand, CancellationToken cancellationToken = default) 
        {
            return Created(string.Empty,await this.Mediator.Send(createIdentityCommand,cancellationToken));
        }

        /// <summary>
        /// Confirm identity
        /// </summary>
        /// <param name="confirmIdentityCommand"></param>
        /// <returns></returns>
        [HttpPost(nameof(ConfirmIdentity))]
        public async Task<IActionResult> ConfirmIdentity([FromBody]ConfirmIdentityCommand confirmIdentityCommand, CancellationToken cancellationToken = default) 
        {
            return Ok(await this.Mediator.Send(confirmIdentityCommand, cancellationToken));
        }     
    }
}