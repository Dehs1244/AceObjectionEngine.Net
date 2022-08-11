using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Model;

namespace AceObjectionEngine.Abstractions
{
    public interface IObjectionAsset
    {
        bool Exists { get; }
        void DownloadAll();
    }

    public interface IObjectionAsset<T> : IObjectionAsset
        where T : IObjectionObject
    {
        IEnumerable<T> Loaded { get; }

        LoaderData<T> Request();

        T Load();
        IEnumerable<T> LoadAll();
    }
}
