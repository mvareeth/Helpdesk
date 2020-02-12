using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model
{
    public class TicketModel
    {
        /// <summary>
        /// The Id for this ticket
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// The title of this ticket
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description for this ticket
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Notes for this ticket
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// status id decides whether it is opened, closed etc.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// technician id working on this ticket
        /// </summary>
        public int AssigedTechnicianId { get; set; }
        /// <summary>
        /// client information of the ticket
        /// </summary>
        public int ClientId { get; set; }
    }
}
