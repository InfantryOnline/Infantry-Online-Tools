using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.InfantryStudio.Assets;

namespace Tools.InfantryStudio.Rendering.Atlas
{
    public class TextureAtlasEntry
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public CfsBitmap CfsBitmap { get; set; }
    }

    /// <summary>
    /// Represents a texture atlas that can be packed with CFS bitmaps.
    /// </summary>
    public class TextureAtlas
    {
        /// <summary>
        /// Gets or sets the unique identifier for this atlas, within a group of atlasses.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the width of this atlas, in pixels.
        /// </summary>
        public int Width { get { return 2048; } }

        /// <summary>
        /// Gets the height of this atlas, in pixels.
        /// </summary>
        public int Height { get { return 2048; } }

        /// <summary>
        /// Returns true if this atlas is locked and no longer accepting new entries.
        /// </summary>
        public bool IsLocked { get { return findingAttempts > 32; } }

        /// <summary>
        /// All the entries in this atlas.
        /// </summary>
        public List<TextureAtlasEntry> Entries = new List<TextureAtlasEntry>();

        /// <summary>
        /// All the points where a new sprite can potentially be inserted.
        /// </summary>
        public List<Point> AvailablePoints = new List<Point>();

        /// <summary>
        /// Attempts to locate a chunk of space on the atlas, and returns an entry if found. Returns null if the dimensions will not fit.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public TextureAtlasEntry FindSpaceForDimensions(int w, int h)
        {
            if (IsLocked)
            {
                // Treat the atlas as "locked", because at this point we are very unlikely to find space.
                return null;
            }

            if (Entries.Count == 0)
            {
                // First time inserting anything, so only check top left.

                if (Width >= w && Height >= h)
                {
                    AvailablePoints.Add(new Point { X = w, Y = 0 });
                    AvailablePoints.Add(new Point { X = 0, Y = h });

                    return new TextureAtlasEntry
                    {
                        X = 0,
                        Y = 0,
                        Width = w,
                        Height = h
                    };
                }
                else
                {
                    // Can't even fit.
                    return null;
                }
            }
            else
            {
                // Move through the atlas one pixel at a time. Worst case, but it will find space if it exists.
                var rects = Entries.Select(e => new Rectangle(e.X, e.Y, e.Width, e.Height));

                // Pick the first available point that fits.

                var foundPointIndex = -1;

                for(var i = 0; i < AvailablePoints.Count; i++)
                {
                    var rect = new Rectangle
                    {
                        X = AvailablePoints[i].X,
                        Y = AvailablePoints[i].Y,
                        Width = w,
                        Height = h
                    };

                    if (rect.X + rect.Width > Width || rect.Y + rect.Height > Height)
                    {
                        continue;
                    }

                    var intersects = rects.Any(r => r.IntersectsWith(rect));

                    if (!intersects)
                    {
                        foundPointIndex = i;
                        break;
                    }
                }

                if (foundPointIndex != -1)
                {
                    var p = AvailablePoints[foundPointIndex];
                    AvailablePoints.RemoveAt(foundPointIndex);

                    AvailablePoints.Add(new Point { X = p.X + w, Y = p.Y });
                    AvailablePoints.Add(new Point { X = p.X, Y = p.Y + h });

                    return new TextureAtlasEntry
                    {
                        X = p.X,
                        Y = p.Y,
                        Width = w,
                        Height = h
                    };
                }
                else
                {
                    findingAttempts++;
                    return null;
                }
            }
        }

        private int findingAttempts = 0;
    }
}
