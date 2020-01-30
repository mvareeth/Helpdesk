using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Management.Tickets
{
    public interface ITicketWriteManager
    {
        Task<TicketDetailModel> SaveTicket(int loginUserId, TicketDetailModel ticket);
    }
}
