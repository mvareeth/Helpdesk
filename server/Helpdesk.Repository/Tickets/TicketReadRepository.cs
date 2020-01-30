using Helpdesk.Data;
using Helpdesk.Entities;
using System.Linq;

namespace Helpdesk.Repository.Tickets
{
    public class TicketReadRepository : ITicketReadRepository
    {
        private readonly IUnitOfWork uow;

        public TicketReadRepository(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IQueryable<Ticket> GetOwnHelpdeskList(int userId)
        {
            var query = GetOwnHelpdeskListQuery(userId);

            return query;
        }

        public IQueryable<Ticket> GetAllHelpdeskList()
        {
            var query = GetAllHelpdeskListQuery();
            return query;
        }
        public Ticket GetTicket(int ticketId)
        {
            var ticket = uow.Query<Ticket>().FirstOrDefault(x => x.Id == ticketId);
            return ticket;
        }

        private IQueryable<Ticket> GetOwnHelpdeskListQuery(int userId)
        {
            return uow.Query<Ticket>()
                .Where(x => x.AssigedTechnicianId == userId);
        }

        private IQueryable<Ticket> GetAllHelpdeskListQuery()
        {
            return uow.Query<Ticket>();
        }

    }
}
