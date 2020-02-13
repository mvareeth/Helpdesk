using Helpdesk.Data;
using Helpdesk.Entities;
using System.Threading.Tasks;

namespace Helpdesk.Repository.Tickets
{
    public class TicketWriteRepository: ITicketWriteRepository
    {
        private readonly IUnitOfWork uow;

        public TicketWriteRepository(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        /// <summary>
        /// add a new ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public async Task<TicketEntity> AddTicket(TicketEntity ticket)
        {
            uow.Add(ticket);
            await uow.CommitAsync();

            return ticket;
        }
        /// <summary>
        /// update the existing ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public async Task<TicketEntity> UpdateTicket(TicketEntity ticket)
        {
            uow.Update(ticket);
            await uow.CommitAsync();

            return ticket;
        }
    }
}
