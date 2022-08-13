using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Extensions;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Abstractions.Layout;

namespace AceObjectionEngine.Engine.Model.Layout
{
    public class ChatBox : IChatBox
    {
        public int Id { get; }

        public ISpriteSource Sprite { get; }
        public string Text { get; }
        public AceSize AnimatorSize { get; }
        public ChatBoxAlign Align { get; }
        public TimeSpan DurationCounter => Sprite.Duration;
        public int FontSize { get; }

        public IAudioSource AudioSource { get; private set; }
        public ICharacter ReferenceCharacter { get; }

        public ChatBox(ICharacter character, ChatBoxSettings settings)
        {
            Id = settings.Id;
            settings.Apply(this);
            AnimatorSize = new AceSize(settings.Width, settings.Height);
            Text = settings.Text;
            Align = settings.Align;
            ReferenceCharacter = character;
            FontSize = settings.FontSize;
            Sprite = _DrawSprite();
            Text = settings.Text;
        }

        public ChatBox(ICharacter character, IObjectionSettings<IChatBox> settings)
        {
            Id = settings.Id;
            settings.Apply(this);
            ReferenceCharacter = character;
            Sprite = _DrawSprite();
        }

        private ISpriteSource _DrawSprite()
        {
            var chatBoxSize = new AceSizeP(AnimatorSize, 95, 25);
            var positionY = Align == ChatBoxAlign.Top ? 15 : (AnimatorSize.Height - chatBoxSize.Height) - 5;
            var positionX = 6;
            Bitmap chatBoxBitmap = new Bitmap(AnimatorSize.Width, AnimatorSize.Height);
            var gifSprite = new GifSprite();
            var printableText = string.Empty;
            for (var i = 0; i < Text.Length; i++)
            {
                var frame = new Bitmap(chatBoxBitmap.Width, chatBoxBitmap.Height);
                var chatBoxBackgroundColor = Color.FromArgb(100, 0, 0, 0); //new ObjectionColor("#000000bf");
                var chatBoxBorderRadiusColor = Color.FromArgb(100, 136, 136, 136);
                var borderRadiusPen = new Pen(chatBoxBorderRadiusColor, 3);

                var namePlateBackgroundColor = Color.FromArgb(255, 5, 5, 100);
                printableText += Text[i].ToString();

                using (Graphics graphics = Graphics.FromImage(frame))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.TextContrast = 11;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    var font = new Font("Arial", 14);
                    Rectangle chatBoxRect = new Rectangle(positionX, positionY, AnimatorSize.Width - 15, chatBoxSize.Height);
                    graphics.FillRoundedRectangle(new SolidBrush(chatBoxBackgroundColor), chatBoxRect, 6);
                    graphics.DrawRoundedRectangle(borderRadiusPen, chatBoxRect, 6);
                    graphics.DrawString(printableText, new Font("Arial", FontSize), Brushes.White, chatBoxRect); //15, positionY);

                    var namePlateSize = graphics.MeasureString(ReferenceCharacter.NamePlate, font);
                    Rectangle namePlateRect = new Rectangle(positionX + 5, positionY - 35, (int)(namePlateSize.Width + 10), (int)(namePlateSize.Height + 10));
                    graphics.FillRoundedRectangle(new SolidBrush(namePlateBackgroundColor), namePlateRect, 6);
                    graphics.DrawRoundedRectangle(borderRadiusPen, namePlateRect, 6);
                    graphics.DrawString(ReferenceCharacter.NamePlate, font, Brushes.White, positionX + 10, positionY - 30);

                }
                borderRadiusPen.Dispose();
                gifSprite.AddFrame(new Sprite(frame));
            }
            //gifSprite.IsFreezeLastOnDelay = true;
            //gifSprite.Delay = TimeSpan.FromSeconds(2);
            AudioSource = new BlipAudioPreset(ReferenceCharacter.Sex).LoadObject().AudioSource;
            AudioSource = AudioSource.SetDuration(Frame.CalculateDuration(1));
            AudioSource = AudioSource.Series(gifSprite.FrameCount);
            AudioSource = AudioSource.SetDuration(Frame.CalculateDuration(gifSprite.FrameCount));

            return gifSprite;
        }

        public void Dispose()
        {
        }

        public void EndAnimation()
        {
            //_character.EndAnimation();
        }

        public async Task EndAnimationAsync() => await Task.Run(EndAnimation);

        public void StartAnimation()
        {
        }

        public async Task StartAnimationAsync() => await Task.Run(StartAnimation);
    }
}
