using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gibbed.Infantry.FileFormats;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Tools.BlobEditor.UserControls
{
    public partial class CfsPreviewControl : UserControl
    {
        public CfsPreviewControl()
        {
            InitializeComponent();
        }

        public void InitializeWithEntryAndStream(BlobFile.Entry entry, Stream stream)
        {
            var dontFixSpecialColors = true;

            sprite = new SpriteFile();
            sprite.Deserialize(stream);

            lblFileName.Text = entry.Name;

            if (!string.IsNullOrWhiteSpace(sprite.Category))
            {
                lblFileName.Text = $"{entry.Name} ({sprite.Category})";
            }

            pictureBoxPreview.Width = sprite.Width;
            pictureBoxPreview.Height = sprite.Height;

            bitmap = new Bitmap(sprite.Width, sprite.Height, PixelFormat.Format8bppIndexed);

            var palette = bitmap.Palette;
            var shadowIndex = 256 - sprite.ShadowCount;
            var lightIndex = shadowIndex - sprite.LightCount;

            for (int i = 0; i < 256; i++)
            {
                var color = sprite.Palette[i];

                var r = (int)((color >> 16) & 0xFF);
                var g = (int)((color >> 8) & 0xFF);
                var b = (int)((color >> 0) & 0xFF);
                //var a = (int)((color >> 24) & 0xFF);

                int a;

                if (i == 0)
                {
                    // transparent pixel
                    a = 0;
                }
                else if (sprite.ShadowCount > 0 && i >= shadowIndex)
                {
                    if (dontFixSpecialColors == false)
                    {
                        // make shadows black+alpha
                        a = 64 + (((i - shadowIndex) + 1) * 16);
                        r = g = b = 0;
                    }
                    else
                    {
                        a = 255;
                    }
                }
                else if (sprite.LightCount > 0 && i >= lightIndex)
                {
                    if (dontFixSpecialColors == false)
                    {
                        // make lights white+alpha
                        a = 64 + (((i - lightIndex) + 1) * 4);
                        r = g = b = 255;
                    }
                    else
                    {
                        a = 255;
                    }
                }
                /*else if (i > sprite.MaxSolidIndex)
                {
                    a = 0;
                }*/
                else
                {
                    a = 255;
                }

                palette.Entries[i] = Color.FromArgb(a, r, g, b);
            }

            this.palette = palette;

            RenderFrameAtIndex(index);

            if (sprite.Frames.Count() == 1)
            {
                btnPlayStop.Enabled = false;
            }
        }

        private void RenderFrameAtIndex(int index)
        {
            var frame = sprite.Frames[index];

            if (frame.Width == 0 || frame.Height == 0)
            {
                return;
            }

            var area = new Rectangle(frame.X, frame.Y, frame.Width, frame.Height);

            bitmap = new Bitmap(sprite.Width, sprite.Height, PixelFormat.Format8bppIndexed);

            var data = bitmap.LockBits(area, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            var scan = data.Scan0;

            for (int o = 0; o < frame.Height * frame.Width; o += frame.Width)
            {
                Marshal.Copy(frame.Pixels, o, scan, frame.Width);
                scan += data.Stride;
            }

            bitmap.UnlockBits(data);

            bitmap.Palette = palette;

            pictureBoxPreview.Image = bitmap;

            lblFrameCount.Text = $"{index} / {sprite.Frames.Length - 1}";
        }

        private void btnPreviousFrame_Click(object sender, EventArgs e)
        {
            if (animationTimer != null)
            {
                isPlaying = false;
                animationTimer.Stop();
            }

            if (index == 0)
            {
                index = sprite.Frames.Length;
            }

            RenderFrameAtIndex(--index);
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            if (animationTimer != null)
            {
                isPlaying = false;
                animationTimer.Stop();
            }

            if (index == sprite.Frames.Length - 1)
            {
                index = -1;
            }

            RenderFrameAtIndex(++index);
        }

        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                animationTimer.Stop();
                isPlaying = false;
            }
            else
            {
                isPlaying = true;

                animationTimer = new Timer();

                var animTime = (sprite.AnimationTime == 0 || sprite.AnimationTime == 10) ? 100 : sprite.AnimationTime;

                animationTimer.Interval = animTime;

                animationTimer.Start();

                animationTimer.Tick += (Object o, EventArgs te) =>
                {
                    if (index == sprite.Frames.Length - 1)
                    {
                        index = -1;
                    }

                    RenderFrameAtIndex(++index);
                };
            }
        }

        Bitmap bitmap;

        SpriteFile sprite;

        ColorPalette palette;

        int index = 0;

        Timer animationTimer;

        bool isPlaying = false;
    }
}
