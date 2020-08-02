using MoreLinq;
using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Tools.InfantryStudio.Rendering
{
    /// <summary>
    /// Represents a single 8x8 pixel tile in the map.
    /// </summary>
    /// <remarks>
    /// Floor tiles repeat the texture indefinitely, so we need to do
    /// (u, v) coordinate fixup or re-calculation when we are trying to
    /// determine where in the atlas we should be getting the 8x8 pixels from.
    /// </remarks>
    public class Tile
    {
        /// <summary>
        /// Index of the lookup for this tile.
        /// </summary>
        public int LookupIndex { get; set; } = -1;

        /// <summary>
        /// The global x coordinate of this tile.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The global y coordinate of this tile.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Entry into the physics atlas for this tile.
        /// </summary>
        public int PhysicsIndex { get; set; }

        /// <summary>
        /// Entry into the vision atlas for this tile.
        /// </summary>
        public int VisionIndex { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TileBlock
    {
        public TileBlock()
        {
            // Initialize all the tiles. Note that they begin with a negative index entry for the sake of
            // not being mapped to anything.

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    Tiles.Add(new Tile
                    {
                        X = X + j,
                        Y = Y + i
                    });
                }
            }
        }

        /// <summary>
        /// The global x coordinate of this block.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The global y coordinate of this block.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// List of the tiles for this block.
        /// </summary>
        public List<Tile> Tiles { get; set; } = new List<Tile>();

        /// <summary>
        /// Converts the global coordinates into block-local and returns the corresponding tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTileAtGlobalCoordinate(int x, int y)
        {
            var blockX = x - X;
            var blockY = y - Y;

            return Tiles[blockY * 64 + blockX];
        }

        /// <summary>
        /// The atlas lookups for this block, just to make it slightly easier to manage because
        /// we have an abundance of tiles but, hopefully, relatively few lookups that are needed,
        /// so rather than scanning all the tiles to do the mapping, we scan this list, which the
        /// tiles actually refer to.
        /// </summary>
        public List<TileAtlasLookup> AtlasLookups { get; set; } = new List<TileAtlasLookup>();

        /// <summary>
        /// The block's resource view.
        /// </summary>
        public ResourceView TextureResourceView { get; set; }

        /// <summary>
        /// The render target view that we render into when we are rendering the block.
        /// </summary>
        public RenderTargetView TextureRenderTargetView { get; set; }

        /// <summary>
        /// The block's texture.
        /// </summary>
        public Texture2D Texture { get; set; }
    }

    public class TileAtlasLookup
    {
        /// <summary>
        /// Index of the atlas that this lookup uses.
        /// </summary>
        public int AtlasIndex { get; set; }

        /// <summary>
        /// Index of the atlas entry that this lookup uses.
        /// </summary>
        public int AtlasEntryIndex { get; set; }

        /// <summary>
        /// X position within the atlas for entry.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y position within the atlas for this entry.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Width of this entry in the atlas.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of this entry in the atlas.
        /// </summary>
        public int Height { get; set; }
    }

    /// <summary>
    /// Renders the tiles of the map.
    /// </summary>
    /// <remarks>
    /// Infantry maps are fixed to 2048x2048 tiles, where each tile is 8x8 pixels for a total of 16384 pixels.
    /// 
    /// We decided to break the map down into blocks of 64x64 tiles, or 512x512 pixels. This means that in total
    /// there are 32x32 blocks.
    /// 
    /// We also use texture atlasses to efficiently pack many floor images into few textures.
    /// 
    /// When a tile is updated, the corresponding block is re-rendered.
    /// 
    /// The editor draws only the blocks that are visible in the viewport.
    /// </remarks>
    public class FloorRenderer
    {
        /// <summary>
        /// Gets the rendering device.
        /// </summary>
        public Renderer Renderer { get; private set; }

        /// <summary>
        /// Full list of blocks.
        /// </summary>
        public List<TileBlock> Blocks { get; set; } = new List<TileBlock>();

        public FloorRenderer(Renderer renderer)
        {
            Renderer = renderer;

            // Initialize all the blocks.

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    Blocks.Add(new TileBlock
                    {
                        X = j * 64,
                        Y = i * 64
                    });
                }
            }
        }

        /// <summary>
        /// Updates a tile at location with the corresponding atlas entry.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="atlasIndex"></param>
        /// <param name="atlasEntryIndex"></param>
        public void UpdateTileAtGlobalCoordinate(int x, int y, int atlasIndex, int atlasEntryIndex)
        {
            var block = GetBlockAtGlobalCoordinate(x, y);
            var tile = block.GetTileAtGlobalCoordinate(x, y);

            // Determine if the block already has this atlas entry, or if we need to add it.

            var lookupIndex = -1;

            var entry = block.AtlasLookups.FirstOrDefault(a => a.AtlasIndex == atlasIndex && a.AtlasEntryIndex == atlasEntryIndex);

            if (entry != null)
            {
                lookupIndex = block.AtlasLookups.IndexOf(entry);
            }
            else
            {
                var atlasEntry = Renderer.FloorAtlasses[atlasIndex].Entries[atlasEntryIndex];

                block.AtlasLookups.Add(new TileAtlasLookup
                {
                    AtlasIndex = atlasIndex,
                    AtlasEntryIndex = atlasEntryIndex,
                    X = atlasEntry.X,
                    Y = atlasEntry.Y,
                    Width = atlasEntry.Width,
                    Height = atlasEntry.Height
                });

                lookupIndex = block.AtlasLookups.Count - 1;
            }

            tile.LookupIndex = lookupIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public TileBlock GetBlockAtGlobalCoordinate(int x, int y)
        {
            var blockX = x / 64;
            var blockY = y / 64;

            return GetBlockAtIndex(blockY * 32 + blockX);
        }

        public TileBlock GetBlockAtIndex(int index)
        {
            return Blocks[index];
        }

        /// <summary>
        /// Renders the block at `index` to the corresponding texture.
        /// </summary>
        /// <param name="index"></param>
        public void RenderBlockAtIndex(int index)
        {
            var block = GetBlockAtIndex(index);

            if (block.AtlasLookups.Count > 8)
            {
                // Absurd.
            }

            // TODO: All the hard work.
        }

        public void RenderBlockAtPosition(int x, int y)
        {
            RenderBlockAtIndex(y * 32 + x);
        }
    }
}
