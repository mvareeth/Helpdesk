using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Security
{
    public interface ISecurityReadManager
    {
        int? ValidateUserCredential(string userName, string password);
    }
}
