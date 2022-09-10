using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Utils;
using System.IO;

namespace AceObjectionEngine.Engine.Infrastructure
{
    /// <summary>
    /// Represents the base for creating setting classes without using presets
    /// </summary>
    /// <typeparam name="T">Objection Object</typeparam>
    public abstract class BaseSettings<T> : IObjectionSettings<T> where T : class, IObjectionObject
    {
        public int Id { get; set; }

        public int Duration { get; }

        public BaseSettings(int id)
        {
            Id = id;
        }

        public BaseSettings()
        {
            Id = -1;
        }

        public BaseSettings(AssetJson json)
        {
            FromJson(json);
        }

        public virtual void ApplyOptions(ref T objection)
        {

        }

        protected string NormalizePath(string path)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                string normalizedPath;
                normalizedPath = path.Replace('\\', Path.DirectorySeparatorChar);
                normalizedPath = path.Replace("\\\\", Path.DirectorySeparatorChar.ToString());
                return normalizedPath;
            }
            else
            {
                return path.Replace('/', Path.DirectorySeparatorChar);
            }
        }

        public void Apply(T objection)
        {
            ObjectionSettingHelpers.ApplyAttributesOfOptions(this);
            ApplyOptions(ref objection);
        }

        public abstract void FromJson(AssetJson json);
    }
}
