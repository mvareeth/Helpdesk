using Helpdesk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Repository.Tickets
{
    public interface ITicketWriteRepository
    {
        Task<TicketEntity> AddTicket(TicketEntity ticket);
        Task<TicketEntity> UpdateTicket(TicketEntity ticket);
    }
}
