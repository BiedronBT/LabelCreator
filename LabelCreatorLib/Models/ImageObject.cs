using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace LabelCreator
{
    public enum ImageRotation
    {
        None,
        Deg90,
        Deg180,
        Deg270
    }

    public class ImageObject : IPasteable, IBitmapConvertible
    {

        private Bitmap _image;
        private int _positionX;
        private int _positionY;
        private ImageRotation _rotation = ImageRotation.None;

        public int Width { get; private set; }
        public int Height { get; private set; }


        internal ImageObject(Bitmap image, int positionX, int positionY)
        {
            _image = image;
            _positionX = positionX;
            _positionY = positionY;
            Width = image.Width;
            Height = image.Height;
        }


        public ImageObject Scale(float percentOfOryginal)
        {
            Width = (int)Math.Round(percentOfOryginal / 100 * Width);
            Height = (int)Math.Round(percentOfOryginal / 100 * Height);

            var newBitmap = new Bitmap(Width, Height);
            using (var graphics = Graphics.FromImage(newBitmap))
                graphics.DrawImage(_image, 0, 0, Width, Height);

            _image = newBitmap;

            return this;
        }


        public ImageObject Rotate(ImageRotation rotation)
        {
            _rotation = rotation;
            Bitmap newBitmap;

            if (rotation == ImageRotation.None || rotation == ImageRotation.Deg180)
                newBitmap = new Bitmap(Width, Height);
            else
                newBitmap = new Bitmap(Height, Width);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                int x = 0;
                int y = 0;

                switch (_rotation)
                {
                    case ImageRotation.None:
                        break;
                    case ImageRotation.Deg90:
                        y = -Height;
                        graphics.RotateTransform(90f);
                        break;
                    case ImageRotation.Deg180:
                        x = -Width;
                        y = -Height;
                        graphics.RotateTransform(180f);
                        break;
                    case ImageRotation.Deg270:
                        x = -Width;
                        graphics.RotateTransform(270f);
                        break;
                }

                graphics.DrawImage(_image,
                                   new RectangleF(x, y, Width, Height),
                                   new RectangleF(0, 0, Width, Height),
                                   GraphicsUnit.Pixel);
            }
            _image = newBitmap;

            return this;
        }


        public Bitmap GetBitmap()
        {
            return _image;
        }


        void IPasteable.Paste(Bitmap label)
        {
            using (Graphics graphics = Graphics.FromImage(label))
            {
                graphics.DrawImage(_image,
                                   new RectangleF(_positionX, _positionY, Width, Height),
                                   new RectangleF(0, 0, Width, Height),
                                   GraphicsUnit.Pixel);
            }
        }


    }
}
