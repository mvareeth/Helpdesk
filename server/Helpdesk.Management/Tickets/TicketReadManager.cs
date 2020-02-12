using Helpdesk.DataMapper;
using Helpdesk.Diagnostics;
using Helpdesk.Entities;
using Helpdesk.Management.Clients;
using Helpdesk.Management.User;
using Helpdesk.Model;
using Helpdesk.Repository.Tickets;
using System.Collections.Generic;
using System.Linq;

namespace Helpdesk.Management.Tickets
{
    /// <summary>
    /// Implement business details of ticket management here.
    /// </summary>
    public class TicketReadManager : ITicketReadManager
    {
        private readonly ITicketReadRepository ticketReadRepository;
        private readonly IUserReadManager userReadManager;
        private readonly IClientReadManager clientReadManager;
        private IDataMapper<TicketListViewModel, TicketEntity> ticketListMapper;
        private IDataMapper<TicketDetailViewModel, TicketEntity> ticketDetailMapper;
        /// <summary>
        /// constructor with different repository, management and mapper interfaces.
        /// </summary>
        /// <param name="ticketReadRepository"></param>
        /// <param name="userReadManager"></param>
        /// <param name="clientReadManager"></param>
        /// <param name="ticketListMapper"></param>
        /// <param name="ticketDetailMapper"></param>
        public TicketReadManager(ITicketReadRepository ticketReadRepository, IUserReadManager userReadManager, IClientReadManager clientReadManager,
            IDataMapper<TicketListViewModel, TicketEntity> ticketListMapper, IDataMapper<TicketDetailViewModel, TicketEntity> ticketDetailMapper)
        {
            this.ticketReadRepository = ticketReadRepository;
            this.userReadManager = userReadManager;
            this.clientReadManager = clientReadManager;
            this.ticketListMapper = ticketListMapper;
            this.ticketDetailMapper = ticketDetailMapper;
        }
        /// <summary>
        /// Returns the own helpdesk tickets
        /// </summary>
        /// <param name="userId">user id - owner of the ticket</param>
        /// <returns>list of tickets</returns>
        public IEnumerable<TicketListViewModel> GetOwnHelpdeskList(int userId)
        {
            var tickets = ticketReadRepository.GetOwnHelpdeskList(userId).ToList();
            var models = ticketListMapper.EntityToModel(tickets);
            return models;
        }
        /// <summary>
        /// get all All helpdesk ticket belongs to that team.
        /// </summary>
        /// <param name="userId">userid to verify whether user is belongs to the team or not</param>
        /// <returns></returns>
        public IEnumerable<TicketListViewModel> GetAllHelpdeskList(int userId)
        {
            int teamId = (int)TeamEnum.Helpdesk;
            var teamMembers = userReadManager.GetTeam(teamId).ToList();

            //if the user belongs the helpdesk team
            if (teamMembers.Any(os => os.UserId == userId))
            {
                var tickets = ticketReadRepository.GetAllHelpdeskList().ToList();
                var models = ticketListMapper.EntityToModel(tickets);
                return models;
            }

            return null;
        }
        /// <summary>
        /// get the details of the ticket.
        /// </summary>
        /// <param name="ticketId">ticket id</param>
        /// <returns></returns>
        public TicketDetailViewModel GetTicket(int ticketId)
        {
            var ticket = ticketReadRepository.GetTicket(ticketId);

            if (ticket == null)
            {
                throw new NotFoundException("Helpdesk ticket is not found");
            }

            var ticketModel = ticketDetailMapper.EntityToModel(ticket);
            if (ticket.AssigedTechnicianId.HasValue)
            {
                ticketModel.AssigedTechnician = userReadManager.GetUser(ticket.AssigedTechnicianId.Value);
            }
            if (ticket.ClientId > 0)
            {
                ticketModel.Client = clientReadManager.GetClientDetail(ticket.ClientId);
            }
            return ticketModel;
        }
    }
}
