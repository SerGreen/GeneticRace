using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotNamespace.Sensors
{
    public abstract class CarSensor
    {
        protected Car car;
        public abstract float getValue();
        public abstract string getName();
    }
}
