using Helpdesk.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Services.Tickets
{
    public interface ITicketWriteService
    {
        Task<IActionResult> SaveTicket([FromBody] TicketDetailModel ticket);
    }
}
