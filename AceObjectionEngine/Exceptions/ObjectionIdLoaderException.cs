using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionIdLoaderException<T> : ObjectionException where T : IObjectionObject
    {
        public ObjectionIdLoaderException(IObjectionAsset<T> asset) : base($"asset {asset} can not be loaded without id")
        {

        }
    }
}
