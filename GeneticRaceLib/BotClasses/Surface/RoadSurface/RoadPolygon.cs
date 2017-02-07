using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GeneticRace.Surface.RoadSurface
{
    public class RoadPolygon : Road
    {
        private Brush brush;

        public RoadPolygon(Polygon polygon, float friction)
        {
            Friction = friction;
            Shape = polygon;

            brush = new SolidBrush(Color.DarkGray);
        }

        public override void paint(System.Drawing.Graphics g)
        {
            Polygon poly = (Polygon)Shape;
            g.FillPolygon(brush, poly.getPointsArray());
        }
    }
}
