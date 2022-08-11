using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AceObjectionEngine.Loader.Utils;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Base Interface for apply settings to Objection Objects
    /// </summary>
    /// <typeparam name="O">Objection Object</typeparam>
    public interface IObjectionSettings
    {
        int Id { get; }
        int Duration { get; }
        void FromJson(AssetJson json);
    }

    /// <summary>
    /// Interface for apply settings to Objection Objects without using presets
    /// </summary>
    /// <typeparam name="O">Objection Object</typeparam>
    public interface IObjectionSettings<O> : IObjectionSettings where O : class, IObjectionObject
    {
        void Apply(O objection);
    }
}
