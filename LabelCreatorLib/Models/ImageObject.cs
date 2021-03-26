using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace LabelCreator
{
    public class ImageObject : IPasteable, IBitmapConvertible
    {

        private Bitmap _image;
        private int _positionX;
        private int _positionY;

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


        public ImageObject Scale(float scale)
        {
            int width = (int)Math.Round(scale * Width);
            int height = (int)Math.Round(scale * Height);
            SetNewImageSize(width, height);
            return this;
        }

        public ImageObject Scale(float widthScale, float heightScale)
        {
            int width = (int)Math.Round(widthScale * Width);
            int height = (int)Math.Round(heightScale * Height);
            SetNewImageSize(width, height);
            return this;
        }

        public ImageObject RotateFlip(RotateFlipType rotateFlipType)
        {
            _image.RotateFlip(rotateFlipType);
            return this;
        }

        public Bitmap GetBitmap()
        {
            return _image;
        }

        private void SetNewImageSize(int width, int height)
        {
            Width = width;
            Height = height;
            var newBitmap = new Bitmap(Width, Height);
            using (var graphics = Graphics.FromImage(newBitmap))
                graphics.DrawImage(_image, 0, 0, Width, Height);

            _image = newBitmap;
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
