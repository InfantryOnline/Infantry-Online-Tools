using Gibbed.Infantry.FileFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.InfantryStudio.Assets;
using Tools.InfantryStudio.Rendering;
using Tools.InfantryStudio.Rendering.Atlas;

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

                // Preallocate all the atlasses needed for this level based on the references.

                List<CfsBitmap> floorCfsBitmaps = file.Floors
                    .Select(f => AssetLibrary.FloorBitmaps
                    .Find(fb => fb.BloFilename == f.FileName && fb.CfsFilename == f.Id))
                    .ToList();

                List<CfsBitmap> objectCfsBitmaps = file.Objects
                    .Select(f => AssetLibrary.ObjectBitmaps
                    .Find(fb => fb.BloFilename == f.FileName && fb.CfsFilename == f.Id))
                    .ToList();

                Renderer.FloorAtlasses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, floorCfsBitmaps);
                Renderer.ObjectAtlasses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, objectCfsBitmaps);

                // Initialize the tiles and objects.


            }
        }

        private void InitializeAssetLibraryCache()
        {
            // TODO: Move asset library initialization to a separate thread so it's not blocking the UI.

            AssetLibrary = new AssetLibrary();
            AssetLibrary.Initialize();

            var floorAtlasses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, AssetLibrary.FloorBitmaps);
            var objectAtlassses = TextureAtlasFactory.CreateAtlassesFromCfsBitmaps(Renderer.RenderingDevice, AssetLibrary.ObjectBitmaps);

            // Debug printout of maps into the working dir.

            for(var i = 0; i < floorAtlasses.Count; i++)
            {
                floorAtlasses[i].Bitmap.Save($"Floor_{i}.png", ImageFormat.Png);
            }

            for (var i = 0; i < objectAtlassses.Count; i++)
            {
                objectAtlassses[i].Bitmap.Save($"Object_{i}.png", ImageFormat.Png);
            }
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
