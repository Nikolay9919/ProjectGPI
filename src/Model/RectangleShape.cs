using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
    [Serializable]
	public class RectangleShape : Shape
	{
    


        public RectangleShape(RectangleF rect) : base(rect)
        {
        }

        public RectangleShape(RectangleShape rectangle) : base(rectangle)
        {
        }

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
            {

                return true;
            }
            else

                return false;
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.DrawRectangle(new Pen(BorderColor, BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

        }
    }
}
