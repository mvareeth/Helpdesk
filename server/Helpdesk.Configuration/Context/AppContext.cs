using System;

namespace Helpdesk.Configuration.Context
{
    public class AppContext : IAppContext
    {
        public int LoggedInUserId { get; set; }
        public string LoggedInUserName { get; set; }
    }
}
