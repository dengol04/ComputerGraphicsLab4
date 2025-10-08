using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ComputerGraphicsLab4
{
    public partial class Form1 : Form
    {
        private List<Polygon> scenePolygons = new List<Polygon>();
        private Polygon currentBuildingPolygon = new Polygon();

        private Button intersectionButton;
        private Button pointInPolygonButton;
        private Button classifyButton;
        private Button normalModeButton;
        private Label modeLabel;

        private enum ToolMode { None, Intersection, PointInPolygon, Classify }
        private ToolMode currentMode = ToolMode.None;

        private PointF? firstEdgeP1 = null;
        private PointF? firstEdgeP2 = null;
        private PointF? secondEdgeP1 = null;
        private PointF? secondEdgeP2 = null;
        private PointF? intersectionPoint = null;

        private PointF? testPoint = null;
        private Tuple<PointF, PointF> testEdge = null;

        private string statusText = "";

        private PointF? tempMouse = null;
        private bool isDragging = false;

        private readonly Color edgeColor = Color.Blue;
        private readonly Color highlightColor = Color.Green;
        private const int vertexRadius = 4;
        private const int vertexDiameter = vertexRadius * 2;
        private const float edgeWidth = 2f;
        private const float highlightWidth = 3f;
        private const float clickTolerance = 5f;

        public Form1()
        {
            InitializeComponent();

            drawingPanel.MouseClick += drawingPanel_MouseClick;
            drawingPanel.Paint += drawingPanel_Paint;
            drawingPanel.MouseDown += DrawingPanel_MouseDown_New;
            drawingPanel.MouseMove += DrawingPanel_MouseMove_New;
            drawingPanel.MouseUp += DrawingPanel_MouseUp_New;
            clearButton.Click += clearButton_Click;

            int top = clearButton.Top;
            int left = Math.Max(10, clearButton.Left - (150 * 4 + 10 * 3));

            intersectionButton = new Button()
            {
                Text = "Пересечение рёбер",
                Width = 150,
                Height = clearButton.Height,
                Location = new Point(left, top)
            };
            intersectionButton.Click += (s, e) => SetMode(ToolMode.Intersection);

            pointInPolygonButton = new Button()
            {
                Text = "Точка в полигоне",
                Width = 150,
                Height = clearButton.Height,
                Location = new Point(intersectionButton.Right + 10, top)
            };
            pointInPolygonButton.Click += (s, e) => SetMode(ToolMode.PointInPolygon);

            classifyButton = new Button()
            {
                Text = "Положение точки",
                Width = 150,
                Height = clearButton.Height,
                Location = new Point(pointInPolygonButton.Right + 10, top)
            };
            classifyButton.Click += (s, e) => SetMode(ToolMode.Classify);

            normalModeButton = new Button()
            {
                Text = "Обычный режим",
                Width = 150,
                Height = clearButton.Height,
                Location = new Point(classifyButton.Right + 10, top)
            };
            normalModeButton.Click += (s, e) => SetMode(ToolMode.None);

            modeLabel = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(normalModeButton.Right + 10, top + 3),
                Text = "Режим: обычный"
            };

            this.Controls.Add(intersectionButton);
            this.Controls.Add(pointInPolygonButton);
            this.Controls.Add(classifyButton);
            this.Controls.Add(normalModeButton);
            this.Controls.Add(modeLabel);

            this.Text = "Создание и Очистка Полигонов + Геометрия";
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void drawingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentMode != ToolMode.None) return;

            Vector2D clickPoint = new Vector2D(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                currentBuildingPolygon.AddVertex(clickPoint);
            }
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
            DrawNewFeatures(e.Graphics);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            scenePolygons.Clear();
            currentBuildingPolygon = new Polygon();

            ResetFeatureState();
            drawingPanel.Invalidate();

            MessageBox.Show("Сцена успешно очищена.", "Очистка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetMode(ToolMode mode)
        {
            currentMode = mode;
            ResetFeatureState();

            switch (mode)
            {
                case ToolMode.Intersection:
                    statusText = "Режим: Пересечение рёбер (ЛКМ — выбрать существующее ребро, затем нажмите и перетащите для второго ребра)";
                    break;
                case ToolMode.PointInPolygon:
                    statusText = "Режим: Проверка точки (ЛКМ — проверить точку)";
                    break;
                case ToolMode.Classify:
                    statusText = "Режим: Классификация (1 — точка, 2 и 3 — ребро)";
                    break;
                case ToolMode.None:
                    statusText = "Режим: обычный (построение полигонов)";
                    break;
            }

            modeLabel.Text = statusText;
            drawingPanel.Invalidate();
        }

        private void DrawingPanel_MouseDown_New(object sender, MouseEventArgs e)
        {
            if (currentMode == ToolMode.None) return;

            PointF click = e.Location;

            if (currentMode == ToolMode.Intersection)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (firstEdgeP1 == null)
                    {
                        //Выбираем существующее ребро
                        var selectedEdge = SelectExistingEdge(click);
                        if (selectedEdge.Item1.X != 0 || selectedEdge.Item1.Y != 0 || selectedEdge.Item2.X != 0 || selectedEdge.Item2.Y != 0)
                        {
                            firstEdgeP1 = selectedEdge.Item1;
                            firstEdgeP2 = selectedEdge.Item2;
                            statusText = "Существующее ребро выбрано. Нажмите и перетащите для второго ребра.";
                        }
                        else
                        {
                            statusText = "Ребро не выбрано. Попробуйте снова.";
                        }
                    }
                    else if (secondEdgeP1 == null)
                    {
                        secondEdgeP1 = click;
                        isDragging = true;
                        statusText = "Начало второго ребра выбрано. Перетащите для динамического рисования.";
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    ResetFeatureState();
                    statusText = "Режим: Пересечение рёбер (ЛКМ — выбрать существующее ребро, затем нажмите и перетащите для второго ребра)";
                }
            }
            else if (currentMode == ToolMode.PointInPolygon)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var poly in scenePolygons) poly.IsHighlighted = false;

                    testPoint = e.Location;
                    var foundPoly = FindPolygonContainingPoint(testPoint.Value);
                    if (foundPoly != null)
                    {
                        var pts = foundPoly.Vertices.Select(v => new PointF((float)v.X, (float)v.Y)).ToList();
                        bool convex = IsConvex(pts);

                        foundPoly.IsHighlighted = true;
                        statusText = $"Точка внутри {(convex ? "выпуклого" : "невыпуклого")} полигона (обводка подсвечена)";
                    }
                    else
                    {
                        statusText = "Точка вне всех полигонов";
                    }
                }
            }
            else if (currentMode == ToolMode.Classify)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (testPoint == null)
                    {
                        testPoint = e.Location;
                        statusText = "Точка выбрана. Теперь выберите 2 точки для ребра.";
                    }
                    else if (firstEdgeP1 == null)
                    {
                        firstEdgeP1 = e.Location;
                        statusText = "Выбрана первая вершина ребра.";
                    }
                    else if (firstEdgeP2 == null)
                    {
                        firstEdgeP2 = e.Location;
                        testEdge = new Tuple<PointF, PointF>(firstEdgeP1.Value, firstEdgeP2.Value);
                        string pos = ClassifyPoint(testPoint.Value, firstEdgeP1.Value, firstEdgeP2.Value);
                        statusText = $"Точка {pos} относительно ребра.";

                        firstEdgeP1 = null;
                        firstEdgeP2 = null;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    testPoint = null;
                    testEdge = null;
                    statusText = "Точка сброшена.";
                }
            }

            modeLabel.Text = statusText;
            drawingPanel.Invalidate();
        }

        private void DrawingPanel_MouseMove_New(object sender, MouseEventArgs e)
        {
            tempMouse = e.Location;

            if (currentMode == ToolMode.Intersection && isDragging && secondEdgeP1 != null)
            {
                secondEdgeP2 = e.Location;
                if (FindIntersection(firstEdgeP1.Value, firstEdgeP2.Value, secondEdgeP1.Value, secondEdgeP2.Value, out PointF inter))
                    intersectionPoint = inter;
                else
                    intersectionPoint = null;

                statusText = intersectionPoint != null ? "Пересечение найдено" : "Пересечения нет ";
                modeLabel.Text = statusText;
                drawingPanel.Invalidate();
            }
        }

        private void DrawingPanel_MouseUp_New(object sender, MouseEventArgs e)
        {
            if (currentMode == ToolMode.Intersection && isDragging && secondEdgeP1 != null)
            {
                secondEdgeP2 = e.Location;
                isDragging = false;
                if (FindIntersection(firstEdgeP1.Value, firstEdgeP2.Value, secondEdgeP1.Value, secondEdgeP2.Value, out PointF inter))
                    intersectionPoint = inter;
                else
                    intersectionPoint = null;

                statusText = intersectionPoint != null ? "Пересечение найдено" : "Пересечения нет";
                modeLabel.Text = statusText;
                drawingPanel.Invalidate();
            }
        }

        private void DrawNewFeatures(Graphics g)
        {
            using (var pen = new Pen(edgeColor, edgeWidth))
            using (var highlightPen = new Pen(highlightColor, highlightWidth))
            using (var brush = new SolidBrush(edgeColor))
            using (var dashed = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                //подсветка выбранного ребра
                if (firstEdgeP1 != null && firstEdgeP2 != null)
                {
                    g.DrawLine(highlightPen, firstEdgeP1.Value, firstEdgeP2.Value);
                    g.FillEllipse(brush, firstEdgeP1.Value.X - vertexRadius, firstEdgeP1.Value.Y - vertexRadius, vertexDiameter, vertexDiameter);
                    g.FillEllipse(brush, firstEdgeP2.Value.X - vertexRadius, firstEdgeP2.Value.Y - vertexRadius, vertexDiameter, vertexDiameter);
                }

                if (secondEdgeP1 != null && secondEdgeP2 != null)
                {
                    g.DrawLine(pen, secondEdgeP1.Value, secondEdgeP2.Value);
                    g.FillEllipse(brush, secondEdgeP1.Value.X - vertexRadius, secondEdgeP1.Value.Y - vertexRadius, vertexDiameter, vertexDiameter);
                    g.FillEllipse(brush, secondEdgeP2.Value.X - vertexRadius, secondEdgeP2.Value.Y - vertexRadius, vertexDiameter, vertexDiameter);
                }
                else if (isDragging && secondEdgeP1 != null && tempMouse != null)
                {
                    //рисуем втрое ребро для пересечения
                    g.DrawLine(dashed, secondEdgeP1.Value, tempMouse.Value);
                    g.FillEllipse(brush, secondEdgeP1.Value.X - vertexRadius, secondEdgeP1.Value.Y - vertexRadius, vertexDiameter, vertexDiameter);
                }

                //точка пересечения
                if (intersectionPoint != null)
                {
                    g.FillEllipse(Brushes.Magenta, intersectionPoint.Value.X - 5, intersectionPoint.Value.Y - 5, 10, 10);
                }

                //Точка для полигонов
                if (testPoint != null)
                {
                    bool inside = scenePolygons.Any(p => p.IsHighlighted);
                    var testBrush = inside ? Brushes.LimeGreen : Brushes.Red;
                    g.FillEllipse(testBrush, testPoint.Value.X - vertexRadius, testPoint.Value.Y - vertexRadius, vertexDiameter, vertexDiameter);
                }

                //Ребро относительно точки
                if (testEdge != null)
                {
                    g.DrawLine(pen, testEdge.Item1, testEdge.Item2);
                    g.FillEllipse(brush, testEdge.Item1.X - vertexRadius, testEdge.Item1.Y - vertexRadius, vertexDiameter, vertexDiameter);
                    g.FillEllipse(brush, testEdge.Item2.X - vertexRadius, testEdge.Item2.Y - vertexRadius, vertexDiameter, vertexDiameter);
                }
            }
            g.DrawString(statusText, new Font("Segoe UI", 9), Brushes.Black, 5, drawingPanel.Height - 20);
        }

        //Сброс состояний
        private void ResetFeatureState()
        {
            firstEdgeP1 = firstEdgeP2 = null;
            secondEdgeP1 = secondEdgeP2 = null;
            intersectionPoint = null;
            testPoint = null;
            testEdge = null;
            tempMouse = null;
            isDragging = false;
            statusText = currentMode == ToolMode.None ? "Режим: обычный" : modeLabel.Text;
            foreach (var poly in scenePolygons) poly.IsHighlighted = false;
        }

        //Функции
        private bool FindIntersection(PointF a1, PointF a2, PointF b1, PointF b2, out PointF intersection)
        {
            intersection = new PointF();
            float x1 = a1.X, y1 = a1.Y, x2 = a2.X, y2 = a2.Y;
            float x3 = b1.X, y3 = b1.Y, x4 = b2.X, y4 = b2.Y;

            float denom = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            if (Math.Abs(denom) < 1e-9) return false;

            float ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / denom;
            float ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / denom;

            if (ua >= 0.0f && ua <= 1.0f && ub >= 0.0f && ub <= 1.0f)
            {
                intersection = new PointF(x1 + ua * (x2 - x1), y1 + ua * (y2 - y1));
                return true;
            }
            return false;
        }

        private Polygon FindPolygonContainingPoint(PointF p)
        {
            foreach (var poly in scenePolygons)
            {
                var pts = poly.Vertices.Select(v => new PointF((float)v.X, (float)v.Y)).ToList();
                if (pts.Count < 3) continue;
                if (PointInPolygon(p, pts))
                    return poly;
            }
            return null;
        }

        private bool PointInPolygon(PointF point, List<PointF> poly)
        {
            bool inside = false;
            for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                var xi = poly[i].X; var yi = poly[i].Y;
                var xj = poly[j].X; var yj = poly[j].Y;

                if (PointOnSegment(new PointF(xi, yi), new PointF(xj, yj), point)) return true;

                bool intersect = ((yi > point.Y) != (yj > point.Y)) &&
                                 (point.X < (xj - xi) * (point.Y - yi) / (yj - yi + 0.0f) + xi);
                if (intersect) inside = !inside;
            }
            return inside;
        }

        private bool PointOnSegment(PointF a, PointF b, PointF p, float eps = 1e-3f)
        {
            float dx = b.X - a.X, dy = b.Y - a.Y;
            float len2 = dx * dx + dy * dy;
            if (len2 == 0) return Distance(a, p) < eps;
            float t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / len2;
            if (t < 0 || t > 1) return false;
            var proj = new PointF(a.X + t * dx, a.Y + t * dy);
            return Distance(proj, p) < clickTolerance;
        }

        private float Distance(PointF a, PointF b)
        {
            float dx = a.X - b.X, dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // Определение выпуклости полигона
        private bool IsConvex(List<PointF> pts)
        {
            if (pts.Count < 4) return true;
            bool? sign = null;
            int n = pts.Count;
            for (int i = 0; i < n; i++)
            {
                float dx1 = pts[(i + 2) % n].X - pts[(i + 1) % n].X;
                float dy1 = pts[(i + 2) % n].Y - pts[(i + 1) % n].Y;
                float dx2 = pts[i].X - pts[(i + 1) % n].X;
                float dy2 = pts[i].Y - pts[(i + 1) % n].Y;
                float cross = dx1 * dy2 - dy1 * dx2;
                if (Math.Abs(cross) < 1e-9) continue;
                if (sign == null) sign = cross > 0;
                else if (sign != (cross > 0)) return false;
            }
            return true;
        }

        private string ClassifyPoint(PointF p, PointF a, PointF b)
        {
            float cross = (b.Y - a.Y) * (p.X - a.X) - (b.X - a.X) * (p.Y - a.Y);

            if (Math.Abs(cross) < 1e-6)
                return "на ребре";
            return cross > 0 ? "слева" : "справа";
        }

        //Выбор ребра
        private Tuple<PointF, PointF> SelectExistingEdge(PointF click)
        {
            foreach (var poly in scenePolygons)
            {
                var vertices = poly.Vertices.Select(v => new PointF((float)v.X, (float)v.Y)).ToList();
                for (int i = 0; i < vertices.Count; i++)
                {
                    var a = vertices[i];
                    var b = vertices[(i + 1) % vertices.Count];
                    if (PointOnSegment(a, b, click))
                    {
                        return new Tuple<PointF, PointF>(a, b);
                    }
                }
            }
            return new Tuple<PointF, PointF>(new PointF(0, 0), new PointF(0, 0));
        }
    }
}