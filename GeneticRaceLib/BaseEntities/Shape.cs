using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BaseEntities
{
    public interface Shape
    {
        bool collidesWith(Polygon poly);
        bool isPointInside(Vector2F p);
    }
}
