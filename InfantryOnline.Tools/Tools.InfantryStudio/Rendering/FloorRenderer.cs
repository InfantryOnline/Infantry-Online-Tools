using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.InfantryStudio.Rendering
{
    /// <summary>
    /// Represents a single 8x8 tile in the map.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Index of the atlas that this tile uses.
        /// </summary>
        public int AtlasIndex { get; set; }

        /// <summary>
        /// Entry into the atlas for this tile.
        /// </summary>
        public int AtlasEntryIndex { get; set; }

        // TODO: Physics, Vision.
    }

    /// <summary>
    /// 
    /// </summary>
    public class RenderableTileBlock
    {
        public List<Tile> Tiles { get; set; } = new List<Tile>();

        // TODO: Texture
    }

    public struct TileAtlasLookup
    {
        public int AtlasIndex { get; set; }

        public float U0 { get; set; }

        public float V0 { get; set; }

        public float U1 { get; set; }

        public float V1 { get; set; }
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
        public RenderingDevice RenderingDevice { get; private set; }

        public List<RenderableTileBlock> Blocks { get; set; } = new List<RenderableTileBlock>();


        public FloorRenderer(RenderingDevice device)
        {
            RenderingDevice = device;
        }

        public void UpdateBlockAtIndex(int index)
        {
            var block = Blocks[index];

            // Construct the lookup tables needed to map the atlas, width and height of the floor textures.

            var atlasLut = new List<TileAtlasLookup>();

            if (atlasLut.Count > 16)
            {
                // Shouldn't happen, but if it does... over budget error.
            }

            // In shader...
            // 1. Create the two triangles for the tile in the geometry shader and write the atlas index and <u, v> coordinates.
            // 2. Sample the texture array of atlasses in pixel shader at the corresponding <u, v> coordinates.
        }
    }
}
