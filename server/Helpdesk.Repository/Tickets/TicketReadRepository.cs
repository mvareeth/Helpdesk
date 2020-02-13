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
        /// <summary>
        /// get list of own tickets
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<TicketEntity> GetOwnHelpdeskList(int userId)
        {
            var query = GetOwnHelpdeskListQuery(userId);

            return query;
        }
        /// <summary>
        /// get all helpdesk tickets in the system
        /// </summary>
        /// <returns></returns>
        public IQueryable<TicketEntity> GetAllHelpdeskList()
        {
            var query = GetAllHelpdeskListQuery();
            return query;
        }
        /// <summary>
        /// get a specified ticket information
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public TicketEntity GetTicket(int ticketId)
        {
            var ticket = uow.Query<TicketEntity>().FirstOrDefault(x => x.Id == ticketId);
            return ticket;
        }
        /// <summary>
        /// filter the tickets based on own id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IQueryable<TicketEntity> GetOwnHelpdeskListQuery(int userId)
        {
            return uow.Query<TicketEntity>()
                .Where(x => x.AssignedTechnicianId == userId);
        }
        /// <summary>
        /// get all tickets without filtering
        /// </summary>
        /// <returns></returns>
        private IQueryable<TicketEntity> GetAllHelpdeskListQuery()
        {
            return uow.Query<TicketEntity>();
        }

    }
}
