using GeneticRace.BotNamespace.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotClasses.Sensors.MaterialDetectors
{
    class CarDetector : CarSensor
    {
        public float angle { get; set; }
        protected float maxDistance;
        protected float step;
        List<Car> cars;
        
        public CarDetector(Car car, float angle, float maxDistance, ref List<Car> cars)
        {
            this.car = car;
            this.angle = angle;
            this.maxDistance = maxDistance;
            this.cars = cars;
            step = 5;
        }

        public override float getValue()
        {
            float distance = 0;
            Vector2F angeledVector = car.DirectionVector.rotate(angle);

            while (distance < maxDistance)
            {
                bool detected = false;
                for (int i = 0; i < cars.Count; i++)
                {
                    Car c = cars[i];
                    if (c != car && c.Body.isPointInside(car.Position + angeledVector * distance))
                    {
                        detected = true;
                        break;
                    }
                }

                if (detected)
                    return distance;

                distance += step;
            }

            return float.PositiveInfinity;
        }

        public override string getName()
        {
            return "Car detector " + (int)(angle * 180 / Math.PI);
        }
    }
}
