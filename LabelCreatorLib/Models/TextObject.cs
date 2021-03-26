using System.Drawing;


namespace LabelCreator
{
    public enum TextDirection 
    {
        Regular,
        UpsideDown,
        TopToBottom,
        BottomToTop
    }

    public class TextObject : IPasteable
    {
        private string _textContent;
        private Font _font;
        private Rectangle _boundries;
        private Brush _color = Brushes.Black;
        private StringAlignment _horizontalAlignment = StringAlignment.Center;
        private StringAlignment _verticalAlignment = StringAlignment.Center;
        private TextDirection _textDirection = TextDirection.Regular;

        private bool _showBoundries = false;
        private Pen _boundriesPen;

        internal TextObject(string textContent, Font textFont, Rectangle textBoundries)
        {
            _textContent = textContent;
            _font = textFont;
            _boundries = textBoundries;
        }


        public TextObject AlignHorizontally(StringAlignment alignment)
        {
            _horizontalAlignment = alignment;
            return this;
        }

        public TextObject AlignVertically(StringAlignment alignment)
        {
            _verticalAlignment = alignment;
            return this;
        }

        public TextObject InColor(Brush color)
        {
            _color = color;
            return this;
        }

        public TextObject InDirection(TextDirection direction)
        {
            _textDirection = direction;
            return this;
        }

        public TextObject ShowBoundries()
        {
            _showBoundries = true;
            _boundriesPen = new Pen(Brushes.Red, 1);
            return this;
        }

        public TextObject ShowBoundries(Brush color, float width)
        {
            _showBoundries = true;
            _boundriesPen = new Pen(color, width);
            return this;
        }


        void IPasteable.Paste(Bitmap label)
        {
            StringFormat format = new StringFormat();
            format.LineAlignment = _verticalAlignment;
            format.Alignment = _horizontalAlignment;

            using (Graphics graphics = Graphics.FromImage(label))
            {
                switch (_textDirection)
                {
                    case TextDirection.Regular:
                        break;
                    case TextDirection.TopToBottom:
                        int ttbw = _boundries.Width;
                        _boundries.Width = _boundries.Height;
                        _boundries.Height = ttbw;
                        int ttbx = _boundries.X;
                        _boundries.X = _boundries.Y;
                        _boundries.Y = -(ttbx + _boundries.Height);
                        graphics.RotateTransform(90f);
                        break;
                    case TextDirection.BottomToTop:
                        int bttw = _boundries.Width;
                        _boundries.Width = _boundries.Height;
                        _boundries.Height = bttw;
                        int bttx = _boundries.X;
                        _boundries.X = -(_boundries.Y + _boundries.Width);
                        _boundries.Y = bttx;
                        graphics.RotateTransform(-90f);
                        break;
                    case TextDirection.UpsideDown:
                        _boundries.X = -(_boundries.X + _boundries.Width);
                        _boundries.Y = -(_boundries.Y + _boundries.Height);
                        graphics.RotateTransform(180f);
                        break;
                }

                if (_showBoundries)
                    graphics.DrawRectangle(_boundriesPen, new Rectangle(_boundries.X, _boundries.Y, _boundries.Width, _boundries.Height));

                graphics.DrawString(_textContent, _font, _color, _boundries, format);
            }
        }


    }
}
