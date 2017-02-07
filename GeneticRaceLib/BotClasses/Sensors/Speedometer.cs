using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotNamespace.Sensors
{
    public class Speedometer : CarSensor
    {
        public Speedometer(Car car)
        {
            this.car = car;
        }

        public override float getValue()
        {
            return car.SpeedVector.getLength();
        }

        public override string getName()
        {
            return "Speedometer";
        }
    }
}
