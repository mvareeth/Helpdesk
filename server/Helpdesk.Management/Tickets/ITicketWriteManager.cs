using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Management.Tickets
{
    public interface ITicketWriteManager
    {
        /// <summary>
        /// To Add or update the ticket
        /// </summary>
        /// <param name="loginUserId">user who create this</param>
        /// <param name="ticketModel">model object</param>
        /// <returns></returns>
        Task<TicketDetailModel> SaveTicket(int loginUserId, TicketDetailModel ticket);
    }
}
