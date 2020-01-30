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
        public async Task<Ticket> SaveTicket(Ticket ticket)
        {
            uow.Add(ticket);
            await uow.CommitAsync();

            return ticket;
        }
    }
}
