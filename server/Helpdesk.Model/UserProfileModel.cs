using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The middle name of the user
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
