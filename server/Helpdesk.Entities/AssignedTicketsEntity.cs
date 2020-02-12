using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Entities
{
    // Todo: let us remove this.
    /// <summary>
    /// The tickets assigned to one user or holding by many users
    /// </summary>
    public class AssignedTicketsEntity
    {
        /// <summary>
        /// The id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The ticket id
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// The start time
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end time
        /// </summary>
        public DateTime End { get; set; }
    }
}
