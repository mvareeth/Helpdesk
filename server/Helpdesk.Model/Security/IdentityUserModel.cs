using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model.Security
{
    public class IdentityUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
    }
}
