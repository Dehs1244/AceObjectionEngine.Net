using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionLoaderException<T> : ObjectionException where T : IObjectionObject
    {
        public ObjectionLoaderException(IObjectionAsset<T> asset) : base($"asset {asset.ToString()} can not be loaded")
        {

        }
    }
}
