using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Configuration
{
    public class AppSettings
    {
        // all apps setting properties will be here.
        public string SASClientURLs { get; set; }
        public string[] SASClientURLArray
        {
            get
            {
                return SASClientURLs.Split(',');
            }
        }
    }
}
