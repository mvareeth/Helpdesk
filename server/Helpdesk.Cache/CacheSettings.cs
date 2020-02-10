using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Cache
{
    public class CacheSettings
    {
        public Dictionary<string, CacheConfigParams> CachedObjects = new Dictionary<string, CacheConfigParams>();

        #region Config Parameters
        public CacheConfigParams Global { get; set; }
        #endregion

        public CacheSettings()
        {
            CreateCacheObjects();
            AddICachetemToCollection();
        }

        private void CreateCacheObjects()
        {
            Global = new CacheConfigParams("");
        }

        private void AddICachetemToCollection()
        {
            CachedObjects.Add(CacheConstants.Global, Global);
        }

        public class CacheConstants
        {
            public const string Global = "Global";
        }
    }



}
