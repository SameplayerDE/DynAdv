using Newtonsoft.Json;
using PatrickAssFucker.Managers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker
{
    public class RelationMatrix<K, KK, V>
    {

        public class ValueSetEventArgs : EventArgs
        {
            public K Key { get; }
            public KK KeyKey { get; }
            public V Value { get; }

            public ValueSetEventArgs(K key, KK keyKey, V value)
            {
                Key = key;
                KeyKey = keyKey;
                Value = value;
            }
        }

        public class ValueCheckedEventArgs : EventArgs
        {
            public K Key { get; }
            public KK KeyKey { get; }

            public ValueCheckedEventArgs(K key, KK keyKey)
            {
                Key = key;
                KeyKey = keyKey;
            }
        }

        public event EventHandler<ValueSetEventArgs> OnValueSet;
        public event EventHandler<ValueCheckedEventArgs> OnValueChecked;

        [JsonProperty("data")]
        private Dictionary<K, Dictionary<KK, V>> _data = new();

        public V? Check(K key, KK keyKey)
        {
            if (_data.TryGetValue(key, out var dict))
            {
                if (dict.TryGetValue(keyKey, out var result))
                {
                    OnValueChecked?.Invoke(this, new ValueCheckedEventArgs(key, keyKey));
                    return result;
                }
            }

            return default;
        }

        public void Set(K key, KK keyKey, V value)
        {
            if (!_data.ContainsKey(key))
            {
                _data[key] = new Dictionary<KK, V>();
            }
            _data[key][keyKey] = value;
            OnValueSet?.Invoke(this, new ValueSetEventArgs(key, keyKey, value));
        }

        public void Set(Dictionary<K, Dictionary<KK, V>> data)
        {
            _data = data;
        }

    }
}
