using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Megarender.WebAPIService.Extensions;

namespace Megarender.WebAPIService.Versions.ANY.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class StatusController : ControllerBase {        
        public StatusController () {
        }

        /// <summary>
        /// Открытый статус
        /// </summary>
        /// <returns></returns>
        [HttpGet (nameof (GetFreeStatus))]
        public IActionResult GetFreeStatus () {
            return Ok (new {
                result=1
            });
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