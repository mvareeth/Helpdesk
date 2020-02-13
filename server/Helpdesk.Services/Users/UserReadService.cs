using Helpdesk.Management.User;
using Helpdesk.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Helpdesk.Services.Users
{
    [Route("api/user")]
    public class UserReadService : BaseService, IUserReadService
	{
        private readonly IUserReadManager userReadManager;
        public UserReadService(IUserReadManager userReadManager)
        {
            this.userReadManager = userReadManager;
        }

        /// <summary>
        /// get the user profile .
        /// </summary>
        /// <returns>get the user detail.</returns>
        [HttpGet("{userId:int}")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetUserDetail(int userId)
        {
            var response = await Task.Run(() => userReadManager.GetUser(userId));
            return new OkObjectResult(response);
        }
        /// <summary>
        /// get the list of clients.
        /// </summary>
        /// <returns>get the client list.</returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetTechnicians()
        {
            var response = await Task.Run(() => userReadManager.GetTechnicians());
            return new OkObjectResult(response);
        }
    }
}
