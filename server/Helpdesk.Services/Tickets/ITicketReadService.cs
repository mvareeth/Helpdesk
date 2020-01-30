using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Services.Tickets
{
    public interface ITicketReadService
    {
        Task<IActionResult> GetTicket(int ticketId);
        Task<IActionResult> GetOwnHelpdeskList();
        Task<IActionResult> GetAllHelpdeskList();
    }
}
