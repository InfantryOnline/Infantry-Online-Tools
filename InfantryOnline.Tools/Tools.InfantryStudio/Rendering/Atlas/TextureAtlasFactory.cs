using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.InfantryStudio.Assets;

namespace Tools.InfantryStudio.Rendering.Atlas
{
    public static class TextureAtlasFactory
    {
        /// <summary>
        /// Returns one or more texture atlasses created from the bitmaps.
        /// </summary>
        /// <remarks>
        /// We do a basic sort based on size of each bitmap, and we pack largest to smallest.
        /// </remarks>
        /// <param name="bitmaps"></param>
        /// <returns></returns>
        public static List<TextureAtlas> CreateAtlassesFromCfsBitmaps(RenderingDevice device, List<CfsBitmap> bitmaps, string cacheFolder)
        {
            var orderedBitmaps = bitmaps
                .DistinctBy(b => b.BloFilename + "#" + b.CfsFilename + "#" + b.FrameIndex)
                .OrderByDescending(b => (b.SpriteFile.Width * b.SpriteFile.Height))
                .ToList();

            // Determine the most that we can pack in a 2048 by 2048 space, since each atlas is that large.

            List<TextureAtlas> output = new List<TextureAtlas>();
            List<TextureAtlas> openList = new List<TextureAtlas>();

            var atlasId = 1;

            openList.Add(new TextureAtlas {Id = atlasId++ });

            foreach(var bitmap in orderedBitmaps)
            {
                bool foundSpace = false;

                foreach(var atlas in openList)
                {
                    var entry = atlas.FindSpaceForDimensions(bitmap.Bitmap.Width, bitmap.Bitmap.Height);

                    if (entry != null)
                    {
                        entry.CfsBitmap = bitmap;
                        atlas.Entries.Add(entry);

                        foundSpace = true;
                        break;
                    }
                }

                if (!foundSpace)
                {
                    var atlas = new TextureAtlas { Id = atlasId++ };
                    var entry = atlas.FindSpaceForDimensions(bitmap.Bitmap.Width, bitmap.Bitmap.Height);

                    if (entry == null)
                    {
                        throw new ApplicationException("Bitmap too large to fit into an atlas. That's a big texture!");
                    }

                    entry.CfsBitmap = bitmap;
                    atlas.Entries.Add(entry);

                    openList.Add(atlas);
                }

                var lockedAtlassses = openList.Where(a => a.IsLocked).ToList();

                // Create bitmaps for each atlas that we have finished, and add it to the cache proper.

                foreach (var atlas in lockedAtlassses)
                {
                    var b = new Bitmap(2048, 2048);

                    using (var gr = Graphics.FromImage(b))
                    {
                        foreach (var entry in atlas.Entries)
                        {
                            gr.DrawImage(entry.CfsBitmap.Bitmap, entry.X, entry.Y);
                            entry.CfsBitmap.Bitmap = null; // No longer needed, free it up.
                        }
                    }

                    b.Save($"{cacheFolder}_{atlas.Id}.png", ImageFormat.Png);

                    openList.Remove(atlas);
                }

                output.AddRange(lockedAtlassses);
            }

            foreach (var atlas in openList)
            {
                var b = new Bitmap(2048, 2048);

                using (var gr = Graphics.FromImage(b))
                {
                    foreach (var entry in atlas.Entries)
                    {
                        gr.DrawImage(entry.CfsBitmap.Bitmap, entry.X, entry.Y);
                        entry.CfsBitmap.Bitmap = null; // No longer needed, free it up.
                    }
                }

                b.Save($"{cacheFolder}_{atlas.Id}.png", ImageFormat.Png);

                openList.Remove(atlas);
            }

            output.AddRange(openList);
            
            return output;
        }
    }
}
