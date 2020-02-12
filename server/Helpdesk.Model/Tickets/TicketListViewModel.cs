using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model
{
    public class TicketListViewModel
    {
        /// <summary>
        /// The Id for this ticket
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of this ticket
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description for this ticket
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The complexity (1-3) of this ticket
        /// </summary>
        public int Complexity { get; set; }

        /// <summary>
        /// Defines priority level; if 1 that is needs to attend first
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// status string value decides whether it is opened, closed etc.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// technicians working on this ticket
        /// </summary>
        public TechnicianModel AssigedTechnician { get; set; }
    }
}
