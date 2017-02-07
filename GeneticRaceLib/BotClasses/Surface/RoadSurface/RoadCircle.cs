using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GeneticRace
{
    public class RoadCircle : Road
    {
        private Brush brush;

        public RoadCircle(Circle circle, float friction)
        {
            Friction = friction > 0 && friction < 1 ? friction : 0.2f;
            Shape = circle;

            brush = new SolidBrush(Color.DarkGray);
        }

        public override void paint(Graphics g)
        {
            Circle circle = (Circle)Shape;
            g.FillEllipse(brush, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2);
        }
    }
}
