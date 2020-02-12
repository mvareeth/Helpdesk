using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Entities
{
    public class TeamEntity
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Id for the team
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
        /// The Id for the user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// decided whether user is team lead or not
        /// </summary>
        public bool IsTeamLead { get; set; }
        /// <summary>
        /// Name of the team
        /// </summary>
        public string TeamName { get; set; }
    }
}
