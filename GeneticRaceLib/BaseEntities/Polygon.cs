using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using GeneticRace.BaseEntities;

namespace GeneticRace
{
    public class Polygon : Shape
    {
        private ArrayList points;

        public Polygon(ArrayList points)
        {
            this.points = points;
        }

        public ArrayList Points
        { get { return points; } }

        public Point[] getPointsArray()
        {
            Point[] array = new Point[points.Count];

            for (int i = 0; i < array.Length; i++)
                array[i] = (Vector2F)points[i];

            return array;
        }

        public bool collidesWith(Rectangle2F rect)
        {
            Vector2F p1 = new Vector2F(rect.X, rect.Y);
            Vector2F p2 = new Vector2F(rect.X + rect.Width, rect.Y);
            Vector2F p3 = new Vector2F(rect.X, rect.Y + rect.Height);
            Vector2F p4 = new Vector2F(rect.X + rect.Width, rect.Y + rect.Height);

            if (isPointInside(p1) || isPointInside(p2) || isPointInside(p3) || isPointInside(p4))
                return true;

            return false;
        }

        public bool collidesWith(Polygon poly)
        {
            foreach (Vector2F p in Points)
                if (poly.isPointInside(p))
                    return true;

            foreach (Vector2F p in poly.Points)
                if (isPointInside(p))
                    return true;

            return false;
        }

        public static Polygon operator +(Polygon a, Vector2F b)
        {
            ArrayList newPoints = new ArrayList();

            foreach (Vector2F p in a.Points)
                newPoints.Add(p + b);

            return new Polygon(newPoints);
        }

        public bool isPointInside(Vector2F p)
        {
            int lastSign = 0;

            for (int i = 0; i < points.Count; i++)
            {
                Vector2F a = (Vector2F)points[i];
                Vector2F b = (Vector2F)points[(i + 1) % points.Count];

                if (i == 0)
                {
                    lastSign = Math.Sign(orient(a, b, p));
                    continue;
                }

                int sign = Math.Sign(orient(a, b, p));

                if (sign != lastSign)
                    return false;

                lastSign = sign;
            }

            return true;
        }

        public int orient(Vector2F a, Vector2F b, Vector2F t)   //on which side of AB line lies point T
        {
            return (int) (t.X * (b.Y - a.Y) + t.Y * (a.X - b.X) + a.Y * b.X - a.X * b.Y);
        }

        public Polygon rotateAround(Vector2F anchor, float angle)
        {
            ArrayList newPoints = new ArrayList();

            foreach (Vector2F p in points)
            {
                newPoints.Add(p.rotateAround(anchor, angle));
            }

            return new Polygon(newPoints);
        }
    }
}
