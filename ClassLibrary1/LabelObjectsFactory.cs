using System.Drawing;
using System.IO;
using LabelCreator.Models;

namespace LabelCreator
{
    public static class LabelObjectsFactory
    {

        public static LabelObject NewLabel(int width, int height)
        {
            return new LabelObject(width, height, Brushes.White);
        }
        
        public static LabelObject NewLabel(int width, int height, Brush color)
        {
            return new LabelObject(width, height, color);
        }

        public static TextObject NewText(string text, Font font, Rectangle boundries)
        {
            return new TextObject(text, font, boundries);
        }

        public static ImageObject NewImage(Bitmap image, int positionX, int positionY)
        {
            return new ImageObject(image, positionX, positionY);
        }

        public static ImageObject NewImage(byte[] image, int positionX, int positionY)
        {
            MemoryStream stream = new MemoryStream(image);
            Bitmap bitmap = new Bitmap(stream);
            return new ImageObject(bitmap, positionX, positionY);
        }

        public static ImageObject NewImage(Image image, int positionX, int positionY)
        {
            return new ImageObject(new Bitmap(image), positionX, positionY);
        }
    }
}
