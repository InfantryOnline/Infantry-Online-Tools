using Gibbed.Infantry.FileFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tools.InfantryStudio.Assets
{
    /// <summary>
    /// Keeps track of a CFS for a given blob, file, and frame index.
    /// </summary>
    /// <remarks>
    /// Objects in the editor are CFS files that most often have more than one frame,
    /// therefore we have to track which is the exact frame that an object refers to.
    /// </remarks>
    public class CfsBitmap
    {
        public string BloFilename { get; set; }

        public string CfsFilename { get; set; }

        public int FrameIndex { get; set; }

        public Bitmap Bitmap { get; set; }

        public SpriteFile SpriteFile { get; set; }

        /// <summary>
        /// Returns one or more CfsBitmaps from a given sprite file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<CfsBitmap> FromSpriteFile(SpriteFile sprite, string blobName, string cfsName)
        {
            List<CfsBitmap> output = new List<CfsBitmap>();

            var bitmap = new Bitmap(sprite.Width, sprite.Height, PixelFormat.Format8bppIndexed);

            var dontFixSpecialColors = true;

            var palette = bitmap.Palette;
            var shadowIndex = 256 - sprite.ShadowCount;
            var lightIndex = shadowIndex - sprite.LightCount;

            // Parse the palette first.

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

            // Next, parse all the frames.

            for (var i = 0; i < sprite.Frames.Count(); i++)
            {
                var frame = sprite.Frames[i];

                if (frame.Width == 0 || frame.Height == 0)
                {
                    continue;
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

                output.Add(new CfsBitmap
                {
                    Bitmap = bitmap,
                    FrameIndex = i,
                    BloFilename = blobName,
                    CfsFilename = cfsName,
                    SpriteFile = sprite
                });
            }

            return output;
        }
    }
}
