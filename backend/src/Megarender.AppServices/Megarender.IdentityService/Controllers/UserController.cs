using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.IdentityService.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace Megarender.IdentityService.Controllers
{    
    [Route ("[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class UserController:ControllerBase
    {
        private readonly ISender _mediator;

        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }   

        /// <summary>
        /// Check status of IS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Status() {
            return Ok();
        }

        /// <summary>
        /// SelfRemove user 
        /// </summary>
        /// <param name="removeUserCommand"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody]RemoveUserCommand removeUserCommand, CancellationToken cancellationToken = default) {
            await _mediator.Send(removeUserCommand, cancellationToken);
            return Ok();
        }     
    }
}