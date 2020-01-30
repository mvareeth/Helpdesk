using Helpdesk.Entities;
using System;
using System.Linq;

namespace Helpdesk.Repository.Tickets
{
    public interface ITicketReadRepository
    {
        public IQueryable<Ticket> GetOwnHelpdeskList(int userId);
        public IQueryable<Ticket> GetAllHelpdeskList();
        public Ticket GetTicket(int ticketId);
    }
}
