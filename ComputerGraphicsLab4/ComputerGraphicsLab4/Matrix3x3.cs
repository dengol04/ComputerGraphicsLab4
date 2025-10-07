using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGraphicsLab4
{
    public class Matrix3x3
    {
        private float[,] matrix;

        public Matrix3x3()
        {
            matrix = new float[3,3];
            CreateMatrix();
        }

        public void CreateMatrix()
        {
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    matrix[i,j] = (i==j) ? 1 : 0;
                }
            }
        }

        public static Matrix3x3 Offset(float dx, float dy)
        {
            Matrix3x3 matrix = new Matrix3x3();
            matrix.matrix[2, 0] = dx;
            matrix.matrix[2, 1] = dy;
            return matrix;
        }

        public static Matrix3x3 Rotation(float Degrees)
        {
            Matrix3x3 matrix = new Matrix3x3();
            float Radians = (float)(Degrees * Math.PI / 180);
            float cos = (float)Math.Cos(Radians);
            float sin = (float)Math.Sin(Radians);

            matrix.matrix[0, 0] = cos;
            matrix.matrix[0, 1] = sin;
            matrix.matrix[1, 0] = -sin;
            matrix.matrix[1, 1] = cos;

            return matrix;
        }

        public static Matrix3x3 Scaling(float sx, float sy)
        {
            Matrix3x3 matrix = new Matrix3x3();
            matrix.matrix[0, 0] = sx;
            matrix.matrix[1, 1] = sy;
            return matrix;
        }

        public Vector2D Multiply(Vector2D point)
        {
            float x = point.X * matrix[0,0] + point.Y * matrix[1,0] + matrix[2,0];
            float y = point.X * matrix[0,1] + point.Y * matrix[1,1] + matrix[2,1];

            return new Vector2D(x, y);
        }

        public static Matrix3x3 Multiply(Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 result = new Matrix3x3(); 
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        result.matrix[i, j] += m1.matrix[i, k] * m2.matrix[k, j];
                    }
                }
            }
            return result;
        }
    }
}
