using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelConstructor.Interfaces;
using LabelConstructor.Models;

namespace LabelCreator.Models
{
    enum TextDirection 
    {
        Regular,
        UpsideDown,
        TopToBottom,
        BottomToTop
    }

    public class TextObject : PasteableObject
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


        
    }
}
