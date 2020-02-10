using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Model.Security
{
    public class TokenModel
    {
        public string AccessToken;
        public int ExpiresIn;
        public string ErrorMessage;
    }
}
