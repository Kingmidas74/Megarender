using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Megarender.WebAPIService.Extensions;
using MediatR;
using Megarender.Business.Modules.PingModule;
using System.Threading.Tasks;
using Megarender.WebAPIService.Middleware;

namespace Megarender.WebAPIService.Versions.ANY.Controllers 
{

    [Route ("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class StatusController : ControllerBase 
    {        
        private readonly ISender mediator;
        public StatusController (ISender mediator) {            
            this.mediator = mediator;
        }

        /// <summary>
        /// Открытый статус
        /// </summary>
        /// <returns></returns>
        [HttpGet (nameof (GetFreeStatus))]
        [Cached(60)]
        public async Task<IActionResult> GetFreeStatus () {
            return Ok (await this.mediator.Send(new CreatePingCommand {
                Message=DateTime.UtcNow.ToLongTimeString()                
            }));
        }

        /// <summary>
        /// Закрытый статус
        /// </summary>
        /// <returns></returns>
        [HttpGet (nameof (GetPrivateStatus))]
        [Authorize]
        public IActionResult GetPrivateStatus () {
            var userId = User.ExtractIdentifier ();
            return Ok (userId);
        }
    }
}