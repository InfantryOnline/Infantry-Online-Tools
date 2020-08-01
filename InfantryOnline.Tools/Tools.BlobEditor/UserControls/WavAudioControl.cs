using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.BlobEditor.UserControls
{
    /// <summary>
    /// Displays the controls for playing back an audio file within the blob.
    /// </summary>
    public partial class WavPreviewControl : UserControl
    {
        public WavPreviewControl()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get { return lblFileName.Text; }
            set { lblFileName.Text = value; }
        }
    }
}
