using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Cache
{
    public class CacheConfigParams
    {
        public CacheConfigParams(string key)
        {
            Key = key;
        }
        public string Key { get; set; }
        public bool IsEnabled { get; set; }
        public int ExpirationTime { get; set; }
        public int SliderTime { get; set; }
    }
}
