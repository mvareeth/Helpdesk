using System;

namespace Helpdesk.Cache
{
    public interface ICache
    {
        object Get(string key);
        bool Add(string key, object value);
        bool Add(string key, object value, int duration, bool isSlider = false, int sliderTime = 0);
        void Remove(string key);
        void ClearAll(int loggedInUserId);

        CacheSettings CacheSettings { get; set; }
    }
}
