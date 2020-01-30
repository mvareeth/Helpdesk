using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Configuration.Context
{
    public interface IAppContext
    {
        int LoggedInUserId { get; set; }
        string LoggedInUserName { get; set; }
    }
}
