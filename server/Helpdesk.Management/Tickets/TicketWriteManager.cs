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
        private readonly ITicketReadRepository ticketReadRepository;
        private readonly ITicketWriteRepository ticketWriteRepository;
        public TicketWriteManager(ITicketReadRepository ticketReadRepository, ITicketWriteRepository ticketWriteRepository)
        {
            this.ticketReadRepository = ticketReadRepository;
            this.ticketWriteRepository = ticketWriteRepository;
        }
        /// <summary>
        /// To Add or update the ticket
        /// </summary>
        /// <param name="loginUserId">user who create this</param>
        /// <param name="ticketModel">model object</param>
        /// <returns></returns>
        public async Task<TicketModel> SaveTicket(int loginUserId, TicketModel ticketModel)
        {
            TicketEntity response = null;
            if (ticketModel.Id.HasValue && ticketModel.Id > 0 )
            {
                response = await Task.Run(() => this.UpdateTicket(loginUserId, ticketModel));
            }
            else
            {
                response = await Task.Run(() => this.AddTicket(loginUserId, ticketModel));
            }

            ticketModel.Id = response?.Id;
            return ticketModel;
        }
        /// <summary>
        /// Add a new ticket
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <param name="ticketModel"></param>
        /// <returns>return the added ticket</returns>
        private async Task<TicketEntity> AddTicket(int loginUserId, TicketModel ticketModel)
        {
            var ticket = this.MapModelToEntity(loginUserId, ticketModel);
            return await Task.Run(() => ticketWriteRepository.AddTicket(ticket));
        }
        /// <summary>
        /// update the ticket status
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <param name="ticketModel"></param>
        /// <returns>return the updated record</returns>
        private async Task<TicketEntity> UpdateTicket(int loginUserId, TicketModel ticketModel)
        {
            var ticket = ticketReadRepository.GetTicket(ticketModel.Id.Value);
            ticket.Title = ticketModel.Title;
            ticket.Description = ticketModel.Description;
            ticket.ClientId = ticketModel.ClientId;
            ticket.StatusId = ticketModel.StatusId;
            ticket.AssignedTechnicianId = ticketModel.AssigedTechnicianId ?? loginUserId;
            ticket.LastUpdatedBy = loginUserId;
            ticket.LastUpdatedDate = DateTime.Now;
            return await Task.Run(() => ticketWriteRepository.UpdateTicket(ticket));
        }

        /// <summary>
        /// method to convert the model to entity
        /// </summary>
        /// <param name="loginUserId">user who create this</param>
        /// <param name="ticketModel">model object</param>
        /// <returns></returns>
        private TicketEntity MapModelToEntity(int loginUserId, TicketModel ticketModel)
        {
            var ticket = new TicketEntity
            {
                ClientId = ticketModel.ClientId,
                Title = ticketModel.Title,
                Description = ticketModel.Description,
                Complexity = 1,
                Priority = 1,
                Notes = ticketModel.Notes,
                CreatedBy = loginUserId,
                CreatedDate = DateTime.Now,
                StatusId = ticketModel.StatusId,
                AssignedTechnicianId = ticketModel.AssigedTechnicianId ??  loginUserId,
            };
            return ticket;
        }
    }
}
