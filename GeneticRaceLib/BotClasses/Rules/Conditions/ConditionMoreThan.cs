using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticRace.BotNamespace.Sensors;

namespace GeneticRace.BotClasses.Rules
{
    public class ConditionMoreThan : Condition
    {
        public ConditionMoreThan(CarSensor sensor, float treshold)
            : base(sensor, treshold)
        { }

        public override bool checkCondition()
        {
            if (sensor.getValue() > treshold)
                return true;

            return false;
        }

        public override string ToString()
        {
            return sensor.getName() + " > " + treshold;
        }
    }
}
