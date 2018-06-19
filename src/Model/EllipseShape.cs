using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Draw.src.Model
{
    [Serializable]
    public class EllipseShape : Shape
    {

        public EllipseShape(RectangleF rect) : base(rect)
        {
        }

        public EllipseShape(RectangleShape rectangle) : base(rectangle)
        {
        }


        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.DrawEllipse(new Pen(BorderColor, BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

        }
    }
}
