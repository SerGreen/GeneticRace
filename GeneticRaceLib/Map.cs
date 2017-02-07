using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GeneticRace
{
    public class Map : DrawableObject
    {
        private ArrayList surfaceObjects;
        public Vector2F StartPoint { get; set; }
        public List<Polygon> CheckPoints { get; set; }

        public Map()
        {
            surfaceObjects = new ArrayList();
        }

        public ArrayList SurfaceObjects
        { get { return surfaceObjects; } }

        public void addSurfaceObject(SurfaceObject so)
        {
            surfaceObjects.Add(so);
        }

        public void paint(System.Drawing.Graphics g)
        {
            foreach (SurfaceObject so in surfaceObjects)
            {
                so.paint(g);
            }
        }
    }
}
