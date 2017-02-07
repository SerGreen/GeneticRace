using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotNamespace.Sensors
{
    public class Compass : CarSensor
    {
        public Compass(Car car)
        {
            this.car = car;
        }

        public override float getValue()
        {
            return car.DirectionVector.AngleOfVector();
        }

        public float getValueDegrees()
        {
            return (float)(getValue() * 180 / Math.PI);
        }

        public override string getName()
        {
            return "Compass";
        }
    }
}
