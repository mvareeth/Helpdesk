using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model.Security
{
    public class TokenModel
    {
        /// <summary>
        /// token string
        /// </summary>
        public string AccessToken;
        /// <summary>
        /// when it will exprire
        /// </summary>
        public int ExpiresIn;
        /// <summary>
        /// error message while creating token
        /// </summary>
        public string ErrorMessage;
    }
}
