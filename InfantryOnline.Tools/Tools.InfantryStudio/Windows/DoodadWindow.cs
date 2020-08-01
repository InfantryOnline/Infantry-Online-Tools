using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.InfantryStudio.Windows
{
    /// <summary>
    /// Displays all the CFS files that can be placed into the map, organized into major categories Floors, Objects, Physics and Vision.
    /// </summary>
    /// <remarks>
    /// Physics and Vision are both hardcoded.
    /// </remarks>
    public partial class DoodadWindow : Form
    {
        public DoodadWindow()
        {
            InitializeComponent();
        }
    }
}
