using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Configuration
{
    public class AppSettings
    {
        // all apps setting properties will be here.
        public string ClientURLs { get; set; }
        public string[] ClientURLArray
        {
            get
            {
                return ClientURLs.Split(',');
            }
        }
    }
}
