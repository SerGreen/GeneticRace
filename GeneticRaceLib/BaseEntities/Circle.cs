using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GeneticRace.BaseEntities;

namespace GeneticRace
{
    public class Circle : Shape
    {
        public Vector2F Center { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2F center, float radius)
        {
            Center = center;
            Radius = radius <= 0 ? 1 : radius;
        }

        public bool collidesWith(Polygon poly)
        {
            foreach (Vector2F p in poly.Points)
            {
                if (Center.getDistanceTo(p) < Radius)
                    return true;
            }

            return false;
        }

        public bool isPointInside(Vector2F p)
        {
            if (Center.getDistanceTo(p) < Radius)
                return true;

            return false;
        }
    }
}
