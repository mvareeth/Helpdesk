using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Tickets
{
    public interface ITicketReadManager
    {
        TicketDetailViewModel GetTicket(int ticketId);
        IEnumerable<TicketListViewModel> GetOwnHelpdeskList(int userId);
        IEnumerable<TicketListViewModel> GetAllHelpdeskList(int userId);
    }
}
