﻿using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Utils;

namespace AceObjectionEngine.Engine.Infrastructure
{
    /// <summary>
    /// Represents the base for creating setting classes without using presets
    /// </summary>
    /// <typeparam name="T">Objection Object</typeparam>
    public abstract class BaseSettings<T> : IObjectionSettings<T> where T : class, IObjectionObject
    {
        public int Id { get; protected set; }

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

        public void Apply(T objection)
        {
            ObjectionSettingHelpers.ApplyAttributesOfOptions(this);
            ApplyOptions(ref objection);
        }

        public abstract void FromJson(AssetJson json);
    }
}