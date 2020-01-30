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
        public int LoginUserId { get; }
    }
}
