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
    public partial class CachingProgressWindow : Form
    {
        public CachingProgressWindow()
        {
            InitializeComponent();
        }

        public void SetTotalProgress(int count)
        {
            progressBar.Maximum = count;
        }

        public void SetCurrentProgress(int count)
        {
            progressBar.Value = count;
        }
    }
}
