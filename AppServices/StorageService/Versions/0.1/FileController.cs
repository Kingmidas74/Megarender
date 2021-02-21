using System.Threading;
using System.Threading.Tasks;
using Megarender.StorageService.CQRS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Megarender.StorageService.Versions.V01.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    [ApiVersion(Constants.V01)]
    [Authorize]
    public class FileController:ControllerBase
    {
        private readonly ISender Mediator;

        public FileController(ISender mediator)
        {
            this.Mediator = mediator;
        }   

        /// <summary>
        /// Upload user file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFileByUser([FromBody]UploadFileByUserCommand uploadFileByUserCommand, CancellationToken cancellationToken = default) {
            return Ok(await this.Mediator.Send(uploadFileByUserCommand));
        }
    }
}
