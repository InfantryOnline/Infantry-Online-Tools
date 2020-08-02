using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.InfantryStudio.Rendering.Atlas;

namespace Tools.InfantryStudio.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class Renderer
    {
        public RenderingDevice RenderingDevice { get; set; }

        public LineRenderer LineRenderer { get; set; }

        public ObjectRenderer ObjectRenderer { get; set; }

        public FloorRenderer FloorRenderer { get; set; }

        public List<TextureAtlas> FloorAtlasses { get; set; } = new List<TextureAtlas>();

        public List<TextureAtlas> ObjectAtlasses { get; set; } = new List<TextureAtlas>();

        public List<TextureAtlas> UserInterfaceAtlasses;

        public void Initialize(Control control)
        {

        }

        public void Render()
        {

        }
    }
}
