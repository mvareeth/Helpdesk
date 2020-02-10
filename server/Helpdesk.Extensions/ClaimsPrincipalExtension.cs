using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Helpdesk.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            return new ClaimsPrincipalHelper().GetLoggedInUserId(principal);
        }
        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            return new ClaimsPrincipalHelper().GetLoggedInUserName(principal);
        }
        public static string GetClientId(this ClaimsPrincipal principal)
        {
            return new ClaimsPrincipalHelper().GetClientId(principal);
        }

    }
    public class ClaimsPrincipalHelper
    {
        object CurrentThread = new object();
        #region Public Methods
        [System.Security.SecuritySafeCritical]
        public int GetLoggedInUserId(ClaimsPrincipal principal)
        {
            lock (CurrentThread)
            {
                int userId = 0;
                ClaimsIdentity claimsIdentity = GetIdentity(principal);
                if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
                {
                    var claim = claimsIdentity.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
                    if (claim != null)
                    {
                        var userIdInClaim = claim.Value;
                        int.TryParse(userIdInClaim, out userId);
                    }
                }
                return userId;
            }
        }


        public string GetLoggedInUserName(ClaimsPrincipal principal)
        {
            string loggedInUserName = string.Empty;
            ClaimsIdentity claimsIdentity = GetIdentity(principal);
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                var claim = claimsIdentity.FindFirst(c => c.Type == ClaimTypes.Name);
                string userName = claim != null ? claim.Value : string.Empty;
                if (!userName.Contains("\\"))
                {
                    return userName;
                }
                if (userName.Split('\\').Length == 2)
                {
                    loggedInUserName = (userName.Split('\\'))[1];
                }
            }
            return loggedInUserName;
        }

        public string GetClientId(ClaimsPrincipal principal)
        {
            string clientId = string.Empty;
            ClaimsIdentity claimsIdentity = GetIdentity(principal);
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                var claim = claimsIdentity.FindFirst(c => c.Type == "clientId");
                if (claim != null)
                {
                    clientId = claim.Value;
                }
            }
            return clientId;
        }
        #endregion

        #region Private Members
        private ClaimsIdentity GetIdentity(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = null;
            if (principal == null) return null;
            claimsIdentity = principal.Identity as ClaimsIdentity;
            return claimsIdentity;
        }
        #endregion
    }
}
