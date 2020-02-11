using Helpdesk.Repository.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Security
{
    public class SecurityReadManager :  ISecurityReadManager
    {
        private readonly ISecurityReadRepository securityReadRepository;
        #region Constructor
        public SecurityReadManager(ISecurityReadRepository securityReadRepository)
        {
            this.securityReadRepository = securityReadRepository; 
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Validate user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>id of the account</returns>
        public int? ValidateUserCredential(string userName, string password)
        {
            return securityReadRepository.GetAccountId(userName,password);
        }


        #endregion
    }
}
