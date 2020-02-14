using Helpdesk.Extensions;
using Helpdesk.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Services.Base
{
    [ApiController]
    [ResponseFilter()]
    public abstract class BaseService : ControllerBase
    {
        private int? _loggedInUserId;
        private string _loggedInUserName;
        /// <summary>
        /// logged-in user ud
        /// </summary>
        public int LoginUserId {
            get
            {
                return _loggedInUserId ?? FindLoggedInUserId();
            } 
        }
        /// <summary>
        /// logged-in user name
        /// </summary>
        public string LoginUserName
        {
            get
            {
                return _loggedInUserName ?? FindLoggedInUserName();
            }
        }
        /// <summary>
        /// get the logged in user id from httpconext
        /// </summary>
        /// <returns></returns>
        private int FindLoggedInUserId()
        {
            if (HttpContext != null)
            {
                _loggedInUserId = HttpContext.User.GetLoggedInUserId();
            }
            return _loggedInUserId ?? 0;
        }
        /// <summary>
        /// get the logged in user name from httpconext
        /// </summary>
        /// <returns></returns>
        private string FindLoggedInUserName()
        {
            if (HttpContext != null)
            {
                _loggedInUserName = HttpContext.User.GetLoggedInUserName();
            }
            return _loggedInUserName ?? null;
        }
    }
}
