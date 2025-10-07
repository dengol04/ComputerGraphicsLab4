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

        private Vector2D? userPoint = null;

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

                    polygonsListBox.Items.Add(currentBuildingPolygon);

                    currentBuildingPolygon = new Polygon();
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                userPoint = clickPoint;

                pointXTextBox.Text = e.X.ToString();
                pointYTextBox.Text = e.Y.ToString();
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

            if (userPoint.HasValue)
            {
                float x = userPoint.Value.X;
                float y = userPoint.Value.Y;
                e.Graphics.FillEllipse(Brushes.Black, x - 5, y - 5, 10, 10);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            scenePolygons.Clear();

            polygonsListBox.Items.Clear();

            currentBuildingPolygon = new Polygon();

            Polygon.ResetIdCounter();

            drawingPanel.Invalidate();

            MessageBox.Show("Сцена успешно очищена.", "Очистка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Polygon GetSelectedPolygon()
        {
            if (polygonsListBox.SelectedItem == null)
            { 
                MessageBox.Show("Пожалуйста, выберите полигон из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return (Polygon)polygonsListBox.SelectedItem;
        }

        private void offsetButton_Click(object sender, EventArgs e)
        {
            Polygon selectedPolygon = GetSelectedPolygon();
            if (selectedPolygon == null) return;

            if (float.TryParse(dxTextBox.Text, out float dx) && float.TryParse(dyTextBox.Text, out float dy))
            {
                Matrix3x3 translationMatrix = Matrix3x3.Offset(dx, dy);
                selectedPolygon.Transform(translationMatrix);
                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Неверный формат dx или dy.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rotateAroundCenterButton_Click(object sender, EventArgs e)
        {
            Polygon selectedPolygon = GetSelectedPolygon();
            if (selectedPolygon == null) return;

            if (float.TryParse(angleTextBox.Text, out float angle))
            {
                Vector2D center = selectedPolygon.GetCenter();
                Matrix3x3 toOrigin = Matrix3x3.Offset(-center.X, -center.Y);
                Matrix3x3 rotation = Matrix3x3.Rotation(angle);
                Matrix3x3 fromOrigin = Matrix3x3.Offset(center.X, center.Y);

                Matrix3x3 finalMatrix = Matrix3x3.Multiply(Matrix3x3.Multiply(toOrigin, rotation), fromOrigin);
                selectedPolygon.Transform(finalMatrix);
                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Неверный формат угла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rotateAroundPointButton_Click(object sender, EventArgs e)
        {
            Polygon selectedPolygon = GetSelectedPolygon();
            if (selectedPolygon == null) return;

            if (float.TryParse(angleTextBox.Text, out float angle) &&
                float.TryParse(pointXTextBox.Text, out float px) &&
                float.TryParse(pointYTextBox.Text, out float py))
            {
                Matrix3x3 toOrigin = Matrix3x3.Offset(-px, -py);
                Matrix3x3 rotation = Matrix3x3.Rotation(angle);
                Matrix3x3 fromOrigin = Matrix3x3.Offset(px, py);

                Matrix3x3 finalMatrix = Matrix3x3.Multiply(Matrix3x3.Multiply(toOrigin, rotation), fromOrigin);
                selectedPolygon.Transform(finalMatrix);
                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Неверный формат угла или координат точки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scaleAroundCenterButton_Click(object sender, EventArgs e)
        {
            Polygon selectedPolygon = GetSelectedPolygon();
            if (selectedPolygon == null) return;

            if (float.TryParse(scaleXTextBox.Text, out float sx) && float.TryParse(scaleYTextBox.Text, out float sy))
            {
                Vector2D center = selectedPolygon.GetCenter();
                Matrix3x3 toOrigin = Matrix3x3.Offset(-center.X, -center.Y);
                Matrix3x3 scaling = Matrix3x3.Scaling(sx, sy);
                Matrix3x3 fromOrigin = Matrix3x3.Offset(center.X, center.Y);

                Matrix3x3 finalMatrix = Matrix3x3.Multiply(Matrix3x3.Multiply(toOrigin, scaling), fromOrigin);
                selectedPolygon.Transform(finalMatrix);
                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Неверный формат коэффициентов масштабирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scaleAroundPointButton_Click(object sender, EventArgs e)
        {
            Polygon selectedPolygon = GetSelectedPolygon();
            if (selectedPolygon == null) return;

            if (float.TryParse(scaleXTextBox.Text, out float sx) &&
                float.TryParse(scaleYTextBox.Text, out float sy) &&
                float.TryParse(pointXTextBox.Text, out float px) &&
                float.TryParse(pointYTextBox.Text, out float py))
            {
                Matrix3x3 toOrigin = Matrix3x3.Offset(-px, -py);
                Matrix3x3 scaling = Matrix3x3.Scaling(sx, sy);
                Matrix3x3 fromOrigin = Matrix3x3.Offset(px, py);

                Matrix3x3 finalMatrix = Matrix3x3.Multiply(Matrix3x3.Multiply(toOrigin, scaling), fromOrigin);
                selectedPolygon.Transform(finalMatrix);
                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Неверный формат коэффициентов или координат точки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
