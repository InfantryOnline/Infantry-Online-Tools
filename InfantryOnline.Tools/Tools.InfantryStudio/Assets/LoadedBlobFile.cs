using Gibbed.Infantry.FileFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.InfantryStudio.Assets
{
    class LoadedBlobFile
    {
        public string BlobName { get; set; }

        public BlobFile BlobFile { get; set; }

        public MemoryStream Stream { get; set; }
    }
}
