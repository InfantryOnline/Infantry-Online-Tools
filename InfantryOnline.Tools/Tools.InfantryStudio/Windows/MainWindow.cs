using Gibbed.Infantry.FileFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.InfantryStudio.Assets;
using Tools.InfantryStudio.Rendering;
using Tools.InfantryStudio.Rendering.Atlas;
using Tools.InfantryStudio.Windows;

namespace Tools.InfantryStudio
{
    /// <summary>
    /// Infantry Studio's main window that displays the map and the menu.
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Gets the asset library.
        /// </summary>
        public AssetLibrary AssetLibrary { get; private set; }

        /// <summary>
        /// Gets the rendering device.
        /// </summary>
        public Renderer Renderer { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Renderer = new Renderer();
            Renderer.Initialize(mapUserControl);

            InitializeAssetLibraryCache();
        }

        public void RunMessageLoop()
        {
            Renderer.Render();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "Infantry Online Level File (*.lvl)|*.lvl";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = new LevelFile();

                var fileName = dialog.SafeFileName;
                var filePath = dialog.FileName;

                var stream = dialog.OpenFile();

                file.Deserialize(stream);

                // Initialize the tiles and objects.


                // Most tiles will only have a handful of atlas lookups, so rather than going through the whole thing,
                // use a dictionary and make quick work of it.
                Dictionary<string, Tuple<int, int>> atlasLookup = new Dictionary<string, Tuple<int, int>>();

                for (int i = 0; i < file.Width; i++)
                {
                    for (int j = 0; j < file.Height; j++)
                    {

                        // TODO: Deal with this. Why are some maps so bloody large?

                        if (i >= 2048 || j >= 2048)
                        {
                            continue;
                        }

                        // Map across the floor array and into the atlas, because we don't care where the data really comes from.

                        var tile = file.Tiles[j * file.Width + i];

                        var blobName = file.Floors[tile.TerrainLookup].FileName.ToLower().Trim();
                        var cfsName = file.Floors[tile.TerrainLookup].Id.ToLower().Trim();

                        if (blobName == null)
                        {
                            // Dealing with a shady file. Deal with this later.
                            throw new NotImplementedException("Deal with this later.");
                        }

                        var dictKey = $"{blobName}#{cfsName}";

                        var atlasIndex = -1;
                        var atlasEntryIndex = -1;

                        if (!atlasLookup.ContainsKey(dictKey))
                        {
                            foreach (var atlas in Renderer.FloorAtlasses)
                            {
                                var foundCfs = atlas.Entries.FirstOrDefault(entry => entry.CfsBitmap.BloFilename == blobName && entry.CfsBitmap.CfsFilename == cfsName);

                                if (foundCfs != null)
                                {
                                    atlasIndex = Renderer.FloorAtlasses.IndexOf(atlas);
                                    atlasEntryIndex = atlas.Entries.IndexOf(foundCfs);

                                    break;
                                }
                            }

                            atlasLookup[dictKey] = new Tuple<int, int>(atlasIndex, atlasEntryIndex);
                        }
                        else
                        {
                            atlasIndex = atlasLookup[dictKey].Item1;
                            atlasEntryIndex = atlasLookup[dictKey].Item2;
                        }

                        if (atlasIndex == -1 || atlasEntryIndex == -1)
                        {
                            throw new NotImplementedException("Atlas entry not found!");
                        }

                        Renderer.FloorRenderer.UpdateTileAtGlobalCoordinate(i, j, atlasIndex, atlasEntryIndex);
                    }
                }


                // TODO: Code below works,but get the tiles to render first.

                //foreach(var entity in file.Entities)
                //{
                //    // Map across the Objects array and into the atlas, because we don't care where the data really comes from.

                //    var obj = file.Objects[entity.ObjectId];

                //    var blobName = obj.FileName.ToLower().Trim();
                //    var cfsName = obj.Id.ToLower().Trim();

                //    if (blobName == null)
                //    {
                //        // Dealing with a shady file. Deal with this later.
                //        throw new NotImplementedException("Deal with this later.");
                //    }

                //    CfsBitmap cfs = null;

                //    foreach (var atlas in Renderer.ObjectAtlasses)
                //    {
                //        var foundCfs = atlas.Entries
                //            .FirstOrDefault(entry => entry.CfsBitmap.BloFilename == blobName
                //            && entry.CfsBitmap.CfsFilename == cfsName
                //            && entry.CfsBitmap.FrameIndex == entity.FrameIndex);

                //        if (foundCfs != null)
                //        {
                //            cfs = foundCfs.CfsBitmap;
                //            break;
                //        }
                //    }

                //    if (cfs == null)
                //    {
                //        throw new NotImplementedException("What do we do here?");
                //    }
                //}
            }
        }

        private void InitializeAssetLibraryCache()
        {
            AssetLibrary = new AssetLibrary();
            AssetLibrary.Initialize();

            Renderer.UserInterfaceAtlasses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, AssetLibrary.UserInterfaceBitmaps, "ui");
            Renderer.FloorAtlasses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, AssetLibrary.FloorBitmaps, "floors");

            // TODO: Code below works, but get the tiles to render first.
            // Renderer.ObjectAtlasses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, AssetLibrary.ObjectBitmaps, "objects");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
