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
        public int LoginUserId {
            get
            {
                return _loggedInUserId ?? FindLoggedInUserId();
            } 
        }

        private int FindLoggedInUserId()
        {
            if (HttpContext != null)
            {
                _loggedInUserId = HttpContext.User.GetLoggedInUserId();
            }
            return _loggedInUserId ?? 0;
        }
    }
}
