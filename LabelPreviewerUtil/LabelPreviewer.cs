﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using LabelCreator;

namespace LabelPreviewerUtil
{
    public class LabelPreviewer
    {

        public static void Preview(params IBitmapConvertible[] images)
        {
            Bitmap[] bitmaps = images.Select(i => i.GetBitmap()).ToArray();
            string[] filesPaths = FileManager.SaveBitmaps(bitmaps);
            ShowWindow(filesPaths);
        }


        private static void ShowWindow(string[] filesPaths)
        {
            var thread = new Thread(() =>
            {
                var window = new ImagePreviewWindow(filesPaths);
                window.Show();
                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();
                Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
