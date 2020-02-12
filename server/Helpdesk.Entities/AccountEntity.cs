using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Entities
{
    /// <summary>
    /// Account information
    /// </summary>
    public class AccountEntity
    {
        /// <summary>
        /// account id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The user name of the account
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The password of the account
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
