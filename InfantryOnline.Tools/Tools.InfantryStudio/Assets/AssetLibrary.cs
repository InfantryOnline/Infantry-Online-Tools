using Gibbed.Infantry.FileFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.InfantryStudio.Assets
{
    /// <summary>
    /// Holds all the assets needed by the map editor.
    /// </summary>
    public class AssetLibrary
    {
        /// <summary>
        /// Load all the assets needed by the editor.
        /// </summary>
        public void Initialize()
        {
            // TODO: Move this into a configuration thing.
            var blobDirectory = "C:\\Program Files (x86)\\Infantry Online";

            FloorBitmaps = Directory
                    .EnumerateFiles(blobDirectory, "*.blo", SearchOption.AllDirectories)
                    .Where(s => Path.GetFileName(s).StartsWith("f_"))
                    .Select(LoadBlobFile)
                    .SelectMany(LoadCfsBitmapFromBlob)
                    .ToList();

            ObjectBitmaps = Directory
                    .EnumerateFiles(blobDirectory, "*.blo", SearchOption.AllDirectories)
                    .Where(s => Path.GetFileName(s).StartsWith("o_"))
                    .Select(LoadBlobFile)
                    .SelectMany(LoadCfsBitmapFromBlob)
                    .ToList();

            UserInterfaceBitmaps = LoadCfsBitmapFromBlob(LoadBlobFile(Path.Combine(blobDirectory, "uiart.blo"))).ToList();
        }

        public List<CfsBitmap> FloorBitmaps { get; set; } = new List<CfsBitmap>();

        public List<CfsBitmap> ObjectBitmaps { get; set; } = new List<CfsBitmap>();

        public List<CfsBitmap> UserInterfaceBitmaps { get; set; } = new List<CfsBitmap>();

        private LoadedBlobFile LoadBlobFile(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var memoryStream = new MemoryStream();

                fs.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var blob = new BlobFile();
                blob.Deserialize(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return new LoadedBlobFile
                {
                    BlobName = Path.GetFileName(path),
                    BlobFile = blob,
                    Stream = memoryStream
                };
            }
        }

        private IEnumerable<CfsBitmap> LoadCfsBitmapFromBlob(LoadedBlobFile blob)
        {
            var entries = blob.BlobFile.Entries
                .Where(e => e.Name.Trim().ToLower().EndsWith(".cfs"))
                .SelectMany(e =>
                {
                    using (var stream = new MemoryStream())
                    {
                        blob.Stream.Seek(e.Offset, SeekOrigin.Begin);
                        blob.Stream.CopyTo(stream, (int)e.Size);

                        stream.Seek(0, SeekOrigin.Begin);

                        var spriteFile = new SpriteFile();
                        spriteFile.Deserialize(stream);

                        return CfsBitmap.FromSpriteFile(spriteFile, blob.BlobName, e.Name);
                    }
                }).ToList();

            blob.Stream.Close();
            blob.Stream.Dispose();

            return entries;
        }
    }
}
