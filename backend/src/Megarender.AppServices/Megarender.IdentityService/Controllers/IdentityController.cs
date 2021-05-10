using System.Threading;
using System.Threading.Tasks;
using Megarender.IdentityService.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Megarender.IdentityService.Controllers
{    
    [Route ("[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class IdentityController:ControllerBase
    {
        private readonly ISender _mediator;

        public IdentityController(ISender mediator)
        {
            _mediator = mediator;
        }   

        /// <summary>
        /// Create new identity
        /// </summary>
        /// <param name="createIdentityCommand"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateIdentity))]
        public async Task<IActionResult> CreateIdentity([FromBody]CreateIdentityCommand createIdentityCommand, CancellationToken cancellationToken = default) 
        {
            return Created(string.Empty,await _mediator.Send(createIdentityCommand,cancellationToken));
        }

        /// <summary>
        /// Confirm identity
        /// </summary>
        /// <param name="confirmIdentityCommand"></param>
        /// <returns></returns>
        [HttpPost(nameof(ConfirmIdentity))]
        public async Task<IActionResult> ConfirmIdentity([FromBody]ConfirmIdentityCommand confirmIdentityCommand, CancellationToken cancellationToken = default) 
        {
            return Ok(await _mediator.Send(confirmIdentityCommand, cancellationToken));
        }     
    }
}