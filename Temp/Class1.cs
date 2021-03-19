using System;
using System.Drawing;
using LabelCreator;
using LabelPreviewerUtil;



namespace Temp
{
    public static class Program
    {
        public static void Main()
        {
            var label = LabelObjectsFactory.NewLabel(500, 500);


            var text = LabelObjectsFactory.NewText("xxx", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Regular), new Rectangle(10, 10, 200, 200));
            text.InColor(Brushes.Black)
                .AlignHorizontally(StringAlignment.Near)
                .AlignVertically(StringAlignment.Center)
                .InDirection(TextDirection.BottomToTop)
                .ShowBoundries();

            label.Paste(text);

            var b = label.GetBitmap();

            //label.SetResolution(300, 300);
            LabelPreviewer.Preview(label);
        }
    }
}
