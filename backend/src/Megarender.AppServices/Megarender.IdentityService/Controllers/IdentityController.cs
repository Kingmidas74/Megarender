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
    public class IdentityController:ControllerBase
    {
        private readonly ISender _mediator;

        public IdentityController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Confirm identity
        /// </summary>
        /// <param name="sendCodeCommand"></param>
        /// <returns>Guid of identity</returns>
        [HttpPost(nameof(SendCode))]
        public async Task<IActionResult> SendCode([FromBody]SendCodeCommand sendCodeCommand, CancellationToken cancellationToken = default) 
        {
            return Ok(
                new
                {
                    id=await _mediator.Send(sendCodeCommand, cancellationToken)
                }
            );
        }
        
        /// <summary>
        /// Confirm identity
        /// </summary>
        /// <param name="verifyCodeCommand"></param>
        /// <returns>Guido of identity</returns>
        [HttpPost(nameof(VerifyCode))]
        public async Task<IActionResult> VerifyCode([FromBody]VerifyCodeCommand verifyCodeCommand, CancellationToken cancellationToken = default) 
        {
            return Ok(
                new
                {
                    password=await _mediator.Send(verifyCodeCommand, cancellationToken)
                }
            );
        }
        
    }
}