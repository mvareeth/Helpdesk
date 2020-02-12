using Helpdesk.Entities;
using System;
using System.Linq;

namespace Helpdesk.Repository.Tickets
{
    public interface ITicketReadRepository
    {
        public IQueryable<TicketEntity> GetOwnHelpdeskList(int userId);
        public IQueryable<TicketEntity> GetAllHelpdeskList();
        public TicketEntity GetTicket(int ticketId);
    }
}
