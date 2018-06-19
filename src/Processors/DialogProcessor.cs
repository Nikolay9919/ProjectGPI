using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		
		
		public DialogProcessor()
		{
		}


        private bool resizeshape;
        public bool ResizeShape
        {
            get { return resizeshape; }
            set { resizeshape = value; }
        }


        /// <summary>
        /// Избран елемент.
        /// </summary>
        private Shape selection;
		public Shape Selection {
			get { return selection; }
			set { selection = value; }
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
        private List<Shape> selectionShape = new List<Shape>();
        public List<Shape> SelectionShape
        {
            get { return selectionShape; }
            set { selectionShape = value; }
        }
        /// <summary>
        /// Последна позиция на мишката при "влачене".
        /// Използва се за определяне на вектора на транслация.
        /// </summary>
        private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}

        public ShapePoint ResizePoint;

        /// <summary>
        /// Добавя примитив - правоъгълник на произволно място върху клиентската област.
        /// </summary>
        public void AddRandomRectangle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 300));
            rect.FillColor = Color.White;
          
            ShapeList.Add(rect);
        }
        public void AddRandomEllispe()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 300));
           
            ellipse.FillColor = Color.White;
           
            ShapeList.Add(ellipse);
        }
        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){						
					return ShapeList[i];
				}	
			}
			return null;
		}
        public void SetFillColor(Color color)
        {
            foreach (var item in SelectionShape)
            {
                item.FillColor = color;
            }
        }
      
       
        public override void Draw(Graphics grfx)
        {
            base.Draw(grfx);
            foreach (var item in selectionShape)
            {
                grfx.DrawRectangle(Pens.Red, item.Rectangle.Left, item.Rectangle.Top, item.Rectangle.Width, item.Rectangle.Height);

            }
        }
    
        public void ChangeCoordinate(PointF p)
        {
            float diffX = p.X - lastLocation.X;
            float diffY = p.Y - lastLocation.Y;
            selection.ChangeCoordinate(ResizePoint.XParam, diffX, diffY);
            lastLocation = p;
        }

        public void Resize(PointF point, Shape rs)
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {

                Shape shape = ShapeList[i];
                RectangleF rectangle = shape.Rectangle;

                var ResizePoint = shape.GetResizePoint(point);

                if (ResizePoint != null)
                {
                    this.ResizePoint = (ShapePoint)ResizePoint;
                    return;
                }
            }
        }
      
        public void SaveAs(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, ShapeList);
        }


        public void Import(OpenFileDialog openFileDialog1)
        {
            Stream myStream = null;
            try
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    using (myStream)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        ShapeList = (List<Shape>)bf.Deserialize(myStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        /// <summary>
        /// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
        /// </summary>
        /// <param name="p">Вектор на транслация.</param>
        public void TranslateTo(PointF p)
		{
			if (selection != null) {
                foreach (var item in SelectionShape)
                {
                    item.Move(p.X - lastLocation.X, p.Y - lastLocation.Y);
                    lastLocation = p;
                }
            }
		}
	}
}
