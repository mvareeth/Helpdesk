using Helpdesk.Data;
using Helpdesk.Entities;
using Helpdesk.Model;
using Helpdesk.Repository.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Management.Tickets
{
    public class TicketWriteManager : ITicketWriteManager
    {
        private readonly ITicketWriteRepository ticketWriteRepository;
        public TicketWriteManager(ITicketWriteRepository ticketWriteRepository)
        {
            this.ticketWriteRepository = ticketWriteRepository;
        }

        public async Task<TicketDetailModel> SaveTicket(int loginUserId, TicketDetailModel ticketModel)
        {
            var ticket = this.MapModelToEntity(loginUserId, ticketModel);
            var response = await Task.Run(() => ticketWriteRepository.SaveTicket(ticket));
            ticketModel.Id = response.Id;
            return ticketModel;
        }

        private Ticket MapModelToEntity(int loginUserId, TicketDetailModel ticketModel)
        {
            var ticket = new Ticket
            {
                ClientId = ticketModel.Client.Id,
                Title = ticketModel.Title,
                Description = ticketModel.Description,
                Complexity = ticketModel.Complexity,

                Priority = ticketModel.Priority,
                Notes = ticketModel.Notes,
                CreatedBy = loginUserId,
                CreatedDate = DateTime.Now,
                LastUpdatedBy = loginUserId,
                LastUpdatedDate = DateTime.Now,
                StatusId = 1,
                AssigedTechnicianId = loginUserId,
            };
            return ticket;
        }
    }
}
