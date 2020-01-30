using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Tickets
{
    public interface ITicketReadManager
    {
        TicketDetailModel GetTicket(int ticketId);
        IEnumerable<TicketListModel> GetOwnHelpdeskList(int userId);
        IEnumerable<TicketListModel> GetAllHelpdeskList(int userId);
    }
}
