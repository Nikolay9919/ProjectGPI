using System;
using System.Drawing;

namespace Draw
{
    /// <summary>
    /// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
    /// </summary>

    [Serializable]
    public class ShapePoint
    {
        public string XParam;
        public string YParam;
        public PointF Point;

        public ShapePoint(string XParam, PointF Point)
        {
            this.XParam = XParam;
            this.Point = Point;
        }
    }
    [Serializable]
    public  class Shape
	{
        #region Constructors
        
        
        public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
       
      
        public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
           
            this.rectangle = shape.rectangle;
			
			this.FillColor =  shape.FillColor;
		}
        #endregion
      
        #region Properties

        /// <summary>
        /// Обхващащ правоъгълник на елемента.
        /// </summary>
        private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}
		
		#endregion
		

		/// <summary>
		/// Проверка дали точка point принадлежи на елемента.
		/// </summary>
		/// <param name="point">Точка</param>
		/// <returns>Връща true, ако точката принадлежи на елемента и
		/// false, ако не пренадлежи</returns>
		public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}
		
		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}
        public virtual void Move(float dx, float dy)
        {
            Location = new PointF(Location.X + dx, Location.Y + dy);
        }

        public virtual double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public virtual object GetResizePoint(PointF point)
        {

            ShapePoint[] destPoints = new ShapePoint[4] {
                new ShapePoint("Left Top", new PointF(Rectangle.Left, Rectangle.Top)),
                new ShapePoint("Right Top",  new PointF(Rectangle.Right, Rectangle.Top)),
                new ShapePoint("Right Bottom",  new PointF(Rectangle.Right, Rectangle.Bottom)),
                new ShapePoint("Left Bottom", new PointF(Rectangle.Left, Rectangle.Bottom))
            };

            for (int i = 0; i < destPoints.Length; i++)
            {
                PointF p = destPoints[i].Point;
                double distance = GetDistance(p.X, p.Y, point.X, point.Y);
                if (distance < 10) return destPoints[i];
            }

            return null;
        }
        internal void ChangeCoordinate(string xParam, float v1, float v2)
        {
            float X = rectangle.X;
            float Y = rectangle.Y;
            float Width = rectangle.Width;
            float Height = rectangle.Height;

            if (xParam == "Left Top")
            {
                Console.WriteLine("dsa");
                rectangle.X += v1;
                rectangle.Y += v2;
                rectangle.Width -= v1;
                rectangle.Height -= v2;
            }
            else if (xParam == "Left Bottom")
            {
                rectangle.X += v1;
                rectangle.Width -= v1;
                rectangle.Height += v2;
            }
            else if (xParam == "Right Top")
            {
                rectangle.Width += v1;
                rectangle.Y += v2;
                rectangle.Height -= v2;
            }
            else if (xParam == "Right Bottom")
            {
                rectangle.Width += v1;
                rectangle.Height += v2;
            }

            if (rectangle.Width <= 15)
            {
                rectangle.X = X;
                rectangle.Width = Width;
            }

            if (rectangle.Height <= 15)
            {
                rectangle.Y = Y;
                rectangle.Height = Height;
            }
        }
    }
}
