using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Megarender.WebAPIService.Extensions;
using System.Threading.Tasks;
using MediatR;
using Megarender.BusinessServices.Modules.UserModule;
using System;
using WebAPIService;
using Megarender.DataStorage;
using System.Threading;

namespace Megarender.WebAPIService.Versions.V01.Controllers {

    [Route ("api/{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(Constants.V01)]
    [Authorize]
    public class FileController : ControllerBase {    

        private readonly ISender mediator;
        private readonly IFileStorage fileStorage;

        public FileController(ISender mediator, IFileStorage fileStorage)
        {
            this.mediator = mediator;
            this.fileStorage = fileStorage;
        }

        /// <summary>
        /// Get all users by organization
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetFile))]
        public async Task<IActionResult> GetFile([FromRoute]string filename, CancellationToken cancellationToken=default) {
            var userId = User.ExtractIdentifier ();
            return Ok(await fileStorage.GetFileAsync(userId.ToString(),filename, cancellationToken));
        }

        /// <summary>
        /// Get info about one user by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateDirectory))]
        public async Task<IActionResult> CreateDirectory(CancellationToken cancellationToken=default) {
            return Ok(await fileStorage.CreateDirectoryAsync(User.ExtractIdentifier().ToString(), cancellationToken));
        }
    }
}