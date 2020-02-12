using Helpdesk.Management.Clients;
using Helpdesk.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Helpdesk.Services.Clients
{
    [Route("api/client")]
    public class ClientReadService : BaseService, IClientReadService
    {
        private readonly IClientReadManager clientReadManager;
        public ClientReadService(IClientReadManager clientReadManager)
        {
            this.clientReadManager = clientReadManager;
        }

        /// <summary>
        /// get the client detail.
        /// </summary>
        /// <returns>get the client detail.</returns>
        [HttpGet("{clientId:int}")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetClientDetail(int clientId)
        {
            var response = await Task.Run(() => clientReadManager.GetClientDetail(clientId));
            return new OkObjectResult(response);
        }
        /// <summary>
        /// get the list of clients.
        /// </summary>
        /// <returns>get the client list.</returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetClients()
        {
            var response = await Task.Run(() => clientReadManager.GetClients());
            return new OkObjectResult(response);
        }

    }
}
