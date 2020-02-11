using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Security
{
    public interface ISecurityReadManager
    {
        /// <summary>
        /// Validate user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>id of the account</returns>
        int? ValidateUserCredential(string userName, string password);
    }
}
