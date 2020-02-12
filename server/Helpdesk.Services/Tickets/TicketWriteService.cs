using Helpdesk.Filters;
using Helpdesk.Management.Tickets;
using Helpdesk.Model;
using Helpdesk.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Services.Tickets
{
    [Route("api/ticket")]
    public class TicketWriteService: BaseService, ITicketWriteService
    {
        private readonly ITicketWriteManager ticketWriteManager;
        public TicketWriteService(ITicketWriteManager ticketWriteManager)
        {
            this.ticketWriteManager = ticketWriteManager;
        }

        [HttpPost("saveTicket")]
        [ValidateModel]
        public async Task<IActionResult> SaveTicket([FromBody] TicketModel ticket)
        {
            var response = await Task.Run(() => ticketWriteManager.SaveTicket(this.LoginUserId, ticket));
            return new OkObjectResult(response);
        }
    }
}
