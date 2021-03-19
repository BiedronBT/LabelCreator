using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace LabelPreviewerUtil
{
    internal class FileManager
    {
        internal static string[] SaveBitmaps(Bitmap[] bitmaps)
        {
            List<string> filesPaths = new List<string>();

            foreach (var bitmap in bitmaps)
            {
                string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Guid.NewGuid().ToString());
                bitmap.Save(savePath, ImageFormat.Png);
                if (File.Exists(savePath))
                    filesPaths.Add(savePath);
            }

            return filesPaths.ToArray();
        }
    }
}
