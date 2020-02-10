using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Security
{
    public class SecurityReadManager :  ISecurityReadManager
    {
        #region Constructor
        public SecurityReadManager()
        {
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Validate user credentials
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int? ValidateUserCredential(string userName, string password)
        {
            return 1001;
        }


        #endregion
    }
}
