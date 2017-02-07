using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticRace.BotNamespace.Sensors;

namespace GeneticRace.BotClasses.Rules
{
    public abstract class Condition
    {
        protected CarSensor sensor;
        public float treshold { get; set; }

        public Condition(CarSensor sensor, float treshold)
        {
            this.sensor = sensor;
            this.treshold = treshold;
        }

        public abstract bool checkCondition();
    }
}
