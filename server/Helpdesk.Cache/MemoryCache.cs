using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Helpdesk.Cache
{
    public class MemCache : ICache
    {
        private readonly int defaultExpirationTime;
        private readonly int defaultSliderTime;
        private readonly bool isEnabledServerCache;
        private IMemoryCache _memoryCache;
        private CacheSettings _cacheSettings;
        public MemCache(IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings)
        {
            _memoryCache = memoryCache;
            CacheSettings = cacheSettings.Value;
            isEnabledServerCache = CacheSettings.Global.IsEnabled;
            defaultExpirationTime = CacheSettings.Global.ExpirationTime;
            defaultSliderTime = CacheSettings.Global.SliderTime;
        }
        public object Get(string key)
        {
            object value;
            if (_memoryCache.TryGetValue(key, out value))
                return value;
            else
                return null;
        }

        public bool Add(string key, object value)
        {
            return AddToCache(key, value, defaultExpirationTime);
        }

        public bool Add(string key, object value, int duration, bool isSlider = false, int sliderTime = 0)
        {
            return AddToCache(key, value, duration, isSlider, sliderTime);
        }

        private bool AddToCache(string key, object value, int duration, bool isSlider = false, int sliderTime = 0)
        {
            bool isThisKeyCachingEnabled = true;

            PropertyInfo keyObject = CacheSettings.GetType().GetProperty(key);


            if (keyObject != null)
            {
                var Obj = CacheSettings.GetType().GetProperty(key).GetValue(CacheSettings, null);
                isThisKeyCachingEnabled = (bool)keyObject.PropertyType.GetProperty("IsEnabled").GetValue(Obj);
            }

            if (isEnabledServerCache == false || isThisKeyCachingEnabled == false)
                return false;
            try
            {
                if (!isSlider)
                {
                    _memoryCache.Set(key, value, new MemoryCacheEntryOptions()
                                                            .SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(duration)));
                }
                else
                {
                    _memoryCache.Set(key, value, new MemoryCacheEntryOptions()
                                                            .SetSlidingExpiration(TimeSpan.FromMinutes(sliderTime > 0 ? sliderTime : defaultSliderTime))
                                                            .SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(duration)));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public CacheSettings CacheSettings
        {
            get
            {
                return _cacheSettings;
            }
            set
            {
                _cacheSettings = value;
            }
        }

        public void ClearAll(int loggedInUserId)
        {
            _memoryCache.Remove(CacheSettings.Global.Key + loggedInUserId.ToString());
        }

    }
}
