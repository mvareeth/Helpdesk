using Helpdesk.Management.Tickets;
using Helpdesk.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Services.Tickets
{
    [Route("api/ticket")]
    public class TicketReadService: BaseService, ITicketReadService
    {
        private readonly ITicketReadManager ticketReadManager;
        public TicketReadService(ITicketReadManager ticketReadManager )
        {
            this.ticketReadManager = ticketReadManager;
        }

        /// <summary>
        /// get the ticket detail.
        /// </summary>
        /// <returns>get the ticket detail.</returns>
        [HttpGet("{ticketId:int}")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetTicket(int ticketId)
        {
            var response = await Task.Run(() => ticketReadManager.GetTicket(ticketId));
            return new OkObjectResult(response);
        }
        /// <summary>
        /// get full list of own tickets.
        /// </summary>
        /// <returns>get own ticket list.</returns>
        [HttpGet("owntickets/{userId:int}")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetOwnHelpdeskList()
        {
            var response = await Task.Run(() => ticketReadManager.GetOwnHelpdeskList(this.LoginUserId));
            return new OkObjectResult(response);
        }
        /// <summary>
        /// get all All helpdesk ticket belongs to that team.
        /// </summary>
          /// <returns></returns>
        [HttpGet("teamtickets/{userId:int}")]
        [ProducesResponseType(typeof(Task<IActionResult>), 200)]
        public async Task<IActionResult> GetAllHelpdeskList()
        {
            var response = await Task.Run(() => ticketReadManager.GetAllHelpdeskList(this.LoginUserId));
            return new OkObjectResult(response);
        }
    }
}
