using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LabelCreator
{
    public class RectangleObjectBase
    {
        protected Rectangle _rectangle;
        protected Pen _borderPen = null;
        protected Brush _filling = null;

        internal RectangleObjectBase(Rectangle rectangle)
        {
            _rectangle = rectangle;
        }

        public RectangleObject WithBorder(Pen borderPen)
        {
            if (this.GetType() == typeof(RectangleObject))
            {
                _borderPen = borderPen;
                return (RectangleObject)this;
            }

            return new RectangleObject(_rectangle, borderPen, null);
        }

        public RectangleObject FilledWith(Brush fillingColor)
        {
            if (this.GetType() == typeof(RectangleObject))
            {
                _filling = fillingColor;
                return (RectangleObject)this;
            }

            return new RectangleObject(_rectangle, null, fillingColor);
        }
    }


    public class RectangleObject : RectangleObjectBase, IPasteable
    {
        internal RectangleObject(Rectangle rectangle, Pen borderPen, Brush filling) : base(rectangle)
        {
            _borderPen = borderPen;
            _filling = filling;
        }

        void IPasteable.Paste(Bitmap label)
        {
            using (Graphics graphics = Graphics.FromImage(label))
            {
                if (_filling != null)
                    graphics.FillRectangle(_filling, _rectangle);

                if (_borderPen != null)
                    graphics.DrawRectangle(_borderPen, _rectangle);
            }
        }
    }
}
