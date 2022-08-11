using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Assets;
using AceObjectionEngine.Abstractions;
using System.Collections.ObjectModel;
using System.IO;

namespace AceObjectionEngine.Loader
{
    public static class GlobalObjectionAssetLoader
    {
        private static IReadOnlyCollection<IObjectionAsset> _assets => new ReadOnlyCollection<IObjectionAsset>(new List<IObjectionAsset>()
        {
            new CharacterLoader(),
            new BackgroundLoader(),
            new MusicLoader(),
            new BubbleLoader(),
            new SoundLoader(),
            new BlipAudioAsset()
        });

        public static void DownloadAll()
        {
            var assets = _assets;
            foreach(var asset in assets)
            {
                asset.DownloadAll();
            }
        }

        public static void DownloadCharacters()
        {
            CharacterLoader loader = new CharacterLoader();
            loader.LoadAll();
        }

        public static void DownloadBackgrounds()
        {
            BackgroundLoader loader = new BackgroundLoader();
            loader.LoadAll();
        }

        public static void DownloadSounds()
        {
            SoundLoader loader = new SoundLoader();
            loader.LoadAll();
        }

        public static void DownloadMusics()
        {
            MusicLoader loader = new MusicLoader();
            loader.LoadAll();
        }
    }
}
