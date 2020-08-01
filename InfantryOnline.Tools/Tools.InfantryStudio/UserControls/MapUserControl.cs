using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.InfantryStudio.UserControls
{
    /// <summary>
    /// Displays the map with scroll bars that allow for movement every 8 pixels (one map tile) in horizontal and vertical directions.
    /// </summary>
    public partial class MapUserControl : UserControl
    {
        public MapUserControl()
        {
            InitializeComponent();

            vScrollbar.Maximum = 2048;
            vScrollbar.Minimum = 0;
            vScrollbar.SmallChange = 8;
            vScrollbar.LargeChange = 8;

            hScrollbar.Maximum = 2048;
            hScrollbar.Minimum = 0;
            hScrollbar.SmallChange = 8;
            hScrollbar.LargeChange = 8;
        }
    }
}
