using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{	
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			InitializeComponent();	
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            if (pickUpSpeedButton.Checked)
            {
                dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
                var shapesel = dialogProcessor.ContainsPoint(e.Location);
                if (dialogProcessor.Selection != null)
                {
                    if (shapesel != null)
                    {
                        if (dialogProcessor.SelectionShape.Contains(shapesel))
                            dialogProcessor.SelectionShape.Remove(shapesel);
                        else
                            dialogProcessor.SelectionShape.Add(shapesel);

                        dialogProcessor.Resize(e.Location, dialogProcessor.Selection);
                        if (dialogProcessor.ResizePoint == null)
                        {
                            dialogProcessor.IsDragging = true;
                            viewPort.Invalidate();
                        }
                        dialogProcessor.LastLocation = e.Location;
                    }
                }

            }
        }

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.ResizePoint != null)
            {
                dialogProcessor.ChangeCoordinate(e.Location);
                viewPort.Invalidate();
            }
            else if (dialogProcessor.IsDragging)
            {
                if (dialogProcessor.SelectionShape == null)
                    dialogProcessor.Resize(e.Location, dialogProcessor.Selection);
                dialogProcessor.TranslateTo(e.Location);
                viewPort.Invalidate();

            }
        }

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
            dialogProcessor.ResizePoint = null;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();

            viewPort.Invalidate();
        }

        

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllispe();

            viewPort.Invalidate();
        }

        

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllispe();

            viewPort.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.SaveAs(saveFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.Import(openFileDialog1);
            }
            viewPort.Invalidate();
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.SetFillColor(colorDialog1.Color);
                viewPort.Invalidate();
            }
        }

    

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewPort_Load(object sender, EventArgs e)
        {

        }

     
    }
}
