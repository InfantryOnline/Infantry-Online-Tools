using Gibbed.Infantry.FileFormats;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.BlobEditor.UserControls;

namespace Tools.BlobEditor
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            outputDevice = new WaveOutEvent();
        }

        private void UpdateMainWindowTitle()
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Text = "Blob Editor";
            }
            else
            {
                Text = $"[{ fileName }, Version { blob.Version }] - Blob Editor";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "Infantry Online Blob File (*.blo)|*.blo";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                blob = new BlobFile();

                fileName = dialog.SafeFileName;
                filePath = dialog.FileName;

                blobStream = dialog.OpenFile();

                blob.Deserialize(blobStream);
                blobListBox.DataSource = blob.Entries;

                UpdateMainWindowTitle();
            }
        }

        private void blobListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = blobListBox.SelectedItem;

            if (item != null)
            {
                var entry = (BlobFile.Entry)item;

                if (entry.Name.ToLower().EndsWith(".wav"))
                {
                    blobStream.Seek(entry.Offset, SeekOrigin.Begin);

                    var waveStream = new MemoryStream((int)entry.Size);

                    blobStream.CopyTo(waveStream, (int)entry.Size);

                    waveStream.Seek(0, SeekOrigin.Begin);

                    var wave = new WaveFileReader(waveStream);

                    outputDevice.Stop();
                    outputDevice.Init(wave);
                    outputDevice.Play();

                    var audioControl = new WavPreviewControl();

                    audioControl.FileName = entry.Name;

                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(audioControl);
                }
                else if (entry.Name.ToLower().EndsWith(".cfs"))
                {
                    blobStream.Seek(entry.Offset, SeekOrigin.Begin);
                    var stream = new MemoryStream((int)entry.Size);

                    blobStream.CopyTo(stream, (int)entry.Size);
                    stream.Seek(0, SeekOrigin.Begin);

                    var control = new CfsPreviewControl();

                    control.InitializeWithEntryAndStream(entry, stream);

                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(control);
                }
                else
                {
                    MessageBox.Show("File format not yet implemented.");
                }
            }
        }

        private WaveOutEvent outputDevice;

        BlobFile blob;

        Stream blobStream;

        string fileName;

        string filePath;
    }
}
