using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BaseEntities
{
    public class Rectangle2F : Shape
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Rectangle2F(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static implicit operator System.Drawing.Rectangle(Rectangle2F rect)
        {
            return new System.Drawing.Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        public static implicit operator Rectangle2F(System.Drawing.Rectangle rect)
        {
            return new Rectangle2F(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public bool collidesWith(Polygon poly)
        {
            Vector2F p1 = new Vector2F(this.X, this.Y);
            Vector2F p2 = new Vector2F(this.X + this.Width, this.Y);
            Vector2F p3 = new Vector2F(this.X, this.Y + this.Height);
            Vector2F p4 = new Vector2F(this.X + this.Width, this.Y + this.Height);

            if (poly.isPointInside(p1) || poly.isPointInside(p2) || poly.isPointInside(p3) || poly.isPointInside(p4))
                return true;

            foreach (Vector2F p in poly.Points)
            {
                if (p.X > X && p.X < X + Width && p.Y > Y && p.Y < Y + Height)
                    return true;
            }

            return false;
        }

        public bool isPointInside(Vector2F p)
        {
            throw new NotImplementedException();
        }
    }
}
