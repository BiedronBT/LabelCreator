using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using LabelCreator.Interfaces;

namespace LabelCreator.Models
{
    public enum ImageRotation
    {
        None,
        Deg90,
        Deg180,
        Deg270
    }

    public class ImageObject : IPasteable
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
            return this;
        }

        public ImageObject Rotate(ImageRotation rotation)
        {
            _rotation = rotation;
            return this;
        }

        void IPasteable.Paste(Bitmap label)
        {
            using (Graphics graphics = Graphics.FromImage(label))
            {
                int x = _positionX;
                int y = _positionY;

                switch (_rotation)
                {
                    case ImageRotation.None:
                        break;
                    case ImageRotation.Deg90:
                        x = _positionY;
                        y = -(_positionX + Height);
                        graphics.RotateTransform(90f);
                        break;
                    case ImageRotation.Deg180:
                        x = -(_positionX + Width);
                        y = -(_positionY + Height);
                        graphics.RotateTransform(180f);
                        break;
                    case ImageRotation.Deg270:
                        x = -(_positionY + Width);
                        y = _positionX;
                        graphics.RotateTransform(270f);
                        break;
                }

                graphics.DrawImage(_image,
                                   new RectangleF(x, y, Width, Height),
                                   new RectangleF(0, 0, Width, Height),
                                   GraphicsUnit.Pixel);
            }
        }


    }
}
