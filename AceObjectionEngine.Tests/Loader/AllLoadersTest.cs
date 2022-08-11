using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Assets;
using AceObjectionEngine.Loader;

namespace AceObjectionEngine.Tests.Loader
{
    public class AllLoadersTest
    {
        [Fact]
        public void DownloadAllAssetsTest()
        {
            GlobalObjectionLoaderSettings.ConfigureAssets((x) =>
            {
                x.Overwrite = false;
            });

            GlobalObjectionAssetLoader.DownloadAll();
        }

        [Fact]
        public void Success()
        {
        }
    }
}
