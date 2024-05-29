using System;

namespace Y.Infrastructure.IApplication
{
    public interface ICookieService
    {

        string Get(string key);

        void Set(string key, string value, int? expireTime);

        void Remove(string key);

    }
}
