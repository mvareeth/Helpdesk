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
        /// <summary>
        /// To Add or update the ticket
        /// </summary>
        /// <param name="loginUserId">user who create this</param>
        /// <param name="ticketModel">model object</param>
        /// <returns></returns>
        public async Task<TicketDetailModel> SaveTicket(int loginUserId, TicketDetailModel ticketModel)
        {
            if (ticketModel.Client == null) {
                ticketModel.Client = new ClientDetailModel();
                ticketModel.Client.Id = 201;
            }
            var ticket = this.MapModelToEntity(loginUserId, ticketModel);
            var response = await Task.Run(() => ticketWriteRepository.SaveTicket(ticket));
            ticketModel.Id = response.Id;
            return ticketModel;
        }

        /// <summary>
        /// method to convert the model to entity
        /// </summary>
        /// <param name="loginUserId">user who create this</param>
        /// <param name="ticketModel">model object</param>
        /// <returns></returns>
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
