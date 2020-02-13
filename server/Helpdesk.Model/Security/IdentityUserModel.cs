using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model.Security
{
    /// <summary>
    /// identity model to pass the login object
    /// </summary>
    public class IdentityUserModel
    {
        /// <summary>
        /// id of the user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ip address of the user 
        /// </summary>
        public string IPAddress { get; set; }
    }
}
