using Helpdesk.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Security.Token
{
    public interface ITokenProvider
    {
        Task<TokenModel> ValidateAndCreateToken(IdentityUserModel identityUser);
    }
}
