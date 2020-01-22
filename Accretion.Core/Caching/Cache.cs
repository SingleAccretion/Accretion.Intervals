using System;
using System.Collections.Generic;
using System.Text;

namespace Accretion.Core
{
    public class Cache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();
        private readonly Func<TKey, TValue> _valuesGenerator;

        public Cache(Func<TKey, TValue> valuesGenerator)
        {
            _valuesGenerator = valuesGenerator;
        }

        public TValue RequestValue(TKey key)
        {            
            if (!_cache.TryGetValue(key, out var value)) 
            {
                value = _valuesGenerator(key);
                _cache.Add(key, value);
            }            

            return value;
        }
    }
}
