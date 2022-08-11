using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Loader;

namespace AceObjectionEngine.Tests
{
    public class SettingsTest
    {
        [Fact]
        public void TestGlobalSettings()
        {
            Assert.NotNull(GlobalObjectionLoaderSettings.CurrentAssets);
            Assert.NotNull(GlobalObjectionLoaderSettings.CurrentPresets);
            GlobalObjectionLoaderSettings.CurrentAssets.Use();
            GlobalObjectionLoaderSettings.CurrentPresets.Use();
        }

        [Fact]
        public void TestGlobalSettingsAsync()
        {
            Assert.NotNull(GlobalObjectionLoaderSettings.CurrentAssets);
            Assert.NotNull(GlobalObjectionLoaderSettings.CurrentPresets);
            GlobalObjectionLoaderSettings.CurrentAssets.UseAsync().Wait();
            GlobalObjectionLoaderSettings.CurrentPresets.UseAsync().Wait();
        }
    }
}
