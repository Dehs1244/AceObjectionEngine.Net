using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Exceptions;

namespace AceObjectionEngine.Loader.Utils
{
    public sealed class AssetJson : IEnumerable<AssetJson>
    {
        private JToken _sourceJson;
        public string RawJson => _sourceJson.ToString();
        public bool IsNullOrEmpty => _sourceJson.Type == JTokenType.Null;

        public bool IsArray => _sourceJson is JArray;
        public bool IsEmptyArray => IsArray && (_sourceJson as JArray).Count < 1;

        public AssetJson(JObject json)
        {
            _sourceJson = json;
        }

        public AssetJson(JToken json)
        {
            _sourceJson = json;
        }

        public AssetJson()
        {
            _sourceJson = new JObject();
        }

        public AssetJson this[object key]
        {
            get
            {
                if (!ContainsKey(key.ToString())) throw new ObjectionReadingJsonException(key);
                return new AssetJson(_sourceJson[key]);
            }
        }

        public IEnumerator<AssetJson> GetEnumerator() => _sourceJson.Select(x => new AssetJson(x)).GetEnumerator();
        public bool ContainsKey(string key) => (_sourceJson as JObject).ContainsKey(key);
        //public bool ContainsKey(object key) => (_sourceJson as JObject).ContainsKey(key);
        public void RemoveByKey(string key) => (_sourceJson as JObject).Remove(key);
        public void Add(string key, object value) => (_sourceJson as JObject).Add(key, JToken.FromObject(value));
        public T ToObject<T>() => _sourceJson.ToObject<T>();

        public void CopyToDictionary(IDictionary<string, object> dictionary, params string[] ignoreKeys)
        {
            foreach(var property in (_sourceJson as JObject).Properties())
            {
                if (dictionary.ContainsKey(property.Name)) continue;
                if (ignoreKeys.Any(x => x == property.Name)) continue;
                dictionary.Add(property.Name, property.Value.ToObject<object>());
            }
        }

        public void CopyToDictionary(IDictionary<string, string> dictionary, params string[] ignoreKeys)
        {
            foreach (var property in (_sourceJson as JObject).Properties())
            {
                if (dictionary.ContainsKey(property.Name)) continue;
                if (ignoreKeys.Any(x => x == property.Name)) continue;
                dictionary.Add(property.Name, property.Value.ToObject<string>());
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return (string)_sourceJson;
        }


        public static implicit operator long(AssetJson asset) => (long)asset._sourceJson;
        public static implicit operator int(AssetJson asset) => (int)asset._sourceJson;
        public static implicit operator bool(AssetJson asset) => (bool)asset._sourceJson;
        public static implicit operator string(AssetJson asset) => (string)asset._sourceJson;
    }
}
