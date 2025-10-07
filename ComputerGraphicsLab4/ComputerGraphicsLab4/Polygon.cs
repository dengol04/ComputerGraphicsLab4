using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab4
{
    public class Polygon
    {
        public List<Vector2D> Vertices { get; private set; }
        public Color PolyColor { get; set; } = Color.Blue;
        public bool IsClosed { get; set; } = false;

        private static int nextId = 1;
        public int Id { get; private set; }

        public Polygon()
        {
            Vertices = new List<Vector2D>();
            Id = nextId++;
        }

        public void AddVertex(Vector2D vertex) => Vertices.Add(vertex);

        public static void ResetIdCounter() => nextId = 1;

        public override string ToString()
        {
            string status = IsClosed ? "Замкнут" : (Vertices.Count == 1 ? "Точка" : Vertices.Count == 2 ? "Ребро" : "В работе");
            return $"Полигон {Id} ({Vertices.Count} в.) - {status}";
        }

        public Vector2D GetCenter()
        {
            float sumX = 0;
            float sumY = 0;
            foreach (Vector2D v in Vertices)
            {
                sumX += v.X;
                sumY += v.Y;
            }
            return new Vector2D(sumX / Vertices.Count, sumY/ Vertices.Count);
        }

        public void Transform(Matrix3x3 matrix)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i] = matrix.Multiply(Vertices[i]);
            }
        }

        public void Draw(Graphics g, bool isBuilding = false)
        {
            if (Vertices.Count == 0) return;

            Color drawColor = isBuilding ? Color.Red : PolyColor;

            using (var pen = new Pen(drawColor, isBuilding ? 3 : 2))
            {
                foreach (var v in Vertices)
                    g.FillEllipse(new SolidBrush(drawColor), v.X - 4, v.Y - 4, 8, 8);

                if (Vertices.Count > 1)
                {
                    for (int i = 0; i < Vertices.Count - 1; i++)
                        g.DrawLine(pen, Vertices[i].X, Vertices[i].Y, Vertices[i + 1].X, Vertices[i + 1].Y);

                    if (IsClosed && Vertices.Count > 2)
                        g.DrawLine(pen, Vertices.Last().X, Vertices.Last().Y, Vertices.First().X, Vertices.First().Y);
                }
            }
        }
    }
}
