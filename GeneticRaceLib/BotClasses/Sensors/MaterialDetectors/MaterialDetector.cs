using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GeneticRace.BotNamespace.Sensors
{
    public abstract class MaterialDetector : CarSensor
    {
        public float angle { get; set; }
        protected float maxDistance;
        protected ArrayList surfaceObjects;
        protected float step;
    }
}
