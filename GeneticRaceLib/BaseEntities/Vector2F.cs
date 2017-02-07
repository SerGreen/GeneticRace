using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace
{
    public class Vector2F
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2F(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator System.Drawing.Point(Vector2F point)
        {
            return new System.Drawing.Point((int) point.X, (int) point.Y);
        }

        public Vector2F rotate(float sinA, float cosA)
        {
            float x = (float)(X * cosA - Y * sinA);
            float y = (float)(Y * cosA + X * sinA);
            return new Vector2F(x, y);
        }

        public Vector2F rotate(float angle)
        {
            return rotate((float)Math.Sin(angle), (float)Math.Cos(angle));
        }

        public Vector2F rotateAround(Vector2F anchor, float angle)
        {
            Vector2F movedPoint = new Vector2F(X - anchor.X, Y - anchor.Y);
            movedPoint = movedPoint.rotate(angle);
            return new Vector2F(movedPoint.X + anchor.X, movedPoint.Y + anchor.Y);
        }

        public override string ToString()
        {
            return "[" + X + ";" + Y + "]";
        }

        public float getDistanceTo(Vector2F p)
        {
            return (float)Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2));
        }

        public static Vector2F operator +(Vector2F a, Vector2F b)
        {
            return new Vector2F(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2F operator -(Vector2F a, Vector2F b)
        {
            return new Vector2F(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2F operator *(Vector2F a, float b)
        {
            return new Vector2F(a.X * b, a.Y * b);
        }

        public static Vector2F AngleVector(float angle)    //единичный вектор, наклонённый на нужный угол
        {
            return new Vector2F((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public float AngleOfVector()
        {
            Vector2F V = this.normalize();
            float a = (float)Math.Acos(V.X);
            if (V.Y < 0) a = (float)(Math.PI * 2 - a);
            return a;
        }

        public Vector2F increaseLength(float dValue)
        {
            float currLength = getLength();
            float newLength = currLength + dValue;

            if (newLength <= 0)
                return new Vector2F(0, 0);

            float coef = newLength / currLength;

            return new Vector2F(X * coef, Y * coef);
        }

        public float getLength()
        { return (float)Math.Sqrt(X * X + Y * Y); }

        public Vector2F normalize()
        {
            float invLength = 1 / getLength();
            return this * invLength;
        }
    }
}
