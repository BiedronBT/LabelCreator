using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LabelCreator.Interfaces;

namespace LabelCreator
{
    public class LabelObject : IBitmapConvertible
    {
        private Bitmap _label;
        private float? _xDpi = default;
        private float? _yDpi = default;

        internal LabelObject(int width, int height, Brush color)
        {
            _label = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(_label))
                graphics.FillRectangle(color, 0, 0, width, height);
        }

        public void Paste(IPasteable obj)
        {
            obj.Paste(_label);
        }


        public LabelObject SetResolution(float xDpi, float yDpi)
        {
            _xDpi = xDpi;
            _yDpi = yDpi;
            return this;
        }

        public Bitmap GetBitmap()
        {
            if (_xDpi != null && _yDpi != null)
                _label.SetResolution((float)_xDpi, (float)_yDpi);
            return _label;
        }
    }
}
