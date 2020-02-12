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

        public IQueryable<TicketEntity> GetOwnHelpdeskList(int userId)
        {
            var query = GetOwnHelpdeskListQuery(userId);

            return query;
        }

        public IQueryable<TicketEntity> GetAllHelpdeskList()
        {
            var query = GetAllHelpdeskListQuery();
            return query;
        }
        public TicketEntity GetTicket(int ticketId)
        {
            var ticket = uow.Query<TicketEntity>().FirstOrDefault(x => x.Id == ticketId);
            return ticket;
        }

        private IQueryable<TicketEntity> GetOwnHelpdeskListQuery(int userId)
        {
            return uow.Query<TicketEntity>()
                .Where(x => x.AssigedTechnicianId == userId);
        }

        private IQueryable<TicketEntity> GetAllHelpdeskListQuery()
        {
            return uow.Query<TicketEntity>();
        }

    }
}
