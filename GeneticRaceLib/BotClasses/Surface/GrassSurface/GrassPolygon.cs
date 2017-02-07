using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GeneticRace.Surface.GrassSurface
{
    public class GrassPolygon : Grass
    {
        private Brush brush;

        public GrassPolygon(Polygon polygon, float friction)
        {
            Friction = friction;
            Shape = polygon;

            brush = new SolidBrush(Color.Green);
        }

        public override void paint(System.Drawing.Graphics g)
        {
            Polygon poly = (Polygon)Shape;
            g.FillPolygon(brush, poly.getPointsArray());
        }
    }
}
