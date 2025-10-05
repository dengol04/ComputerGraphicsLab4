using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab4
{
    public partial class Form1 : Form
    {
        private List<Polygon> scenePolygons = new List<Polygon>();

        private Polygon currentBuildingPolygon = new Polygon();

        public Form1()
        {
            InitializeComponent();

            drawingPanel.MouseClick += drawingPanel_MouseClick;
            drawingPanel.Paint += drawingPanel_Paint;

            clearButton.Click += clearButton_Click;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            this.Text = "Создание и Очистка Полигонов";
        }

        private void drawingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Vector2D clickPoint = new Vector2D(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
                currentBuildingPolygon.AddVertex(clickPoint);
            else if (e.Button == MouseButtons.Right)
            {
                if (currentBuildingPolygon.Vertices.Count > 0)
                {
                    if (currentBuildingPolygon.Vertices.Count >= 3)
                        currentBuildingPolygon.IsClosed = true;

                    scenePolygons.Add(currentBuildingPolygon);

                    currentBuildingPolygon = new Polygon();
                }
            }

            drawingPanel.Invalidate();
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var polygon in scenePolygons)
                polygon.Draw(e.Graphics, isBuilding: false);
            
            if (currentBuildingPolygon.Vertices.Count > 0)
                currentBuildingPolygon.Draw(e.Graphics, isBuilding: true);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            scenePolygons.Clear();

            currentBuildingPolygon = new Polygon();

            Polygon.ResetIdCounter();

            drawingPanel.Invalidate();

            MessageBox.Show("Сцена успешно очищена.", "Очистка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
