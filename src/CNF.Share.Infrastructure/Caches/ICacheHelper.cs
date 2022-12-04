using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Infrastructure.Caches
{
    public interface ICacheHelper
    {
        bool Exists(string key);

        void Set<T>(string key, T value);
        void Set<T>(string key, T value, TimeSpan timeSpan);

        T Get<T>(string key);
        object Get(string key);

        void Remove(string key);

        T GetOrSet<T>(string key, Func<T> getDataCallback, TimeSpan? exp = null);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getDataCallback, TimeSpan? exp = null);
    }
}
