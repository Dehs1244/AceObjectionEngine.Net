using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AceObjectionEngine.Loader.Model
{
    public class PresetMetaData : Dictionary<string, object>
    {
        [JsonIgnore]
        public int Id => int.Parse(this["id"].ToString());
        [JsonIgnore]
        public string Name => this["name"].ToString();

        public PresetMetaData(int id, string name)
        {
            Add("id", id);
            Add("name", name);
        }

        [JsonConstructor]
        public PresetMetaData()
        {

        }

        public T ToObjectFromKey<T>(string key) => ToJson()[key].ToObject<T>();

        public AssetJson ToJson() => new AssetJson(JObject.FromObject(this));
    }
}
