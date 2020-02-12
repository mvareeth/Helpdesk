using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Helpdesk.Model
{
    public class TicketDetailViewModel
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
        [DataType(DataType.MultilineText)]
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
        /// Notes for this ticket
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        /// <summary>
        /// The userId who created it
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// The created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The userId who updated last
        /// </summary>
        public int LastUpdatedBy { get; set; }
        /// <summary>
        /// The last updated date
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// The date closed
        /// </summary>
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// The user id who closed it
        /// </summary>
        public int ClosedBy { get; set; }
        /// <summary>
        /// status id decides whether it is opened, closed etc.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// technicians working on this ticket
        /// </summary>
        public UserProfileModel AssigedTechnician { get; set; }
        /// <summary>
        /// client information of the ticket
        /// </summary>
        public ClientDetailViewModel Client { get; set; }
    }
}
