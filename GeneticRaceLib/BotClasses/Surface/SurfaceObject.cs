using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GeneticRace.BaseEntities;

namespace GeneticRace
{
    public abstract class SurfaceObject : DrawableObject
    {
        public Shape Shape { get; set; }
        public float Friction { get; set; }

        public abstract void paint(Graphics g);
    }
}
