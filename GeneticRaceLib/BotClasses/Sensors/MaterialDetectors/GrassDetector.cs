using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GeneticRace.BotNamespace.Sensors.MaterialDetectors
{
    public class GrassDetector : MaterialDetector
    {
        public GrassDetector(Car car, float angle, float maxDistance, ArrayList surfaceObjects)
        {
            this.car = car;
            this.angle = angle;
            this.maxDistance = maxDistance;
            this.surfaceObjects = surfaceObjects;
            step = 5;
        }

        public override float getValue()
        {
            float distance = 0;
            Vector2F angeledVector = car.DirectionVector.rotate(angle);

            while (distance < maxDistance)
            {
                bool detected = false;
                for (int i = 0; i < surfaceObjects.Count; i++)
                {
                    SurfaceObject so = (SurfaceObject)surfaceObjects[i];
                    if (so.Shape.isPointInside(car.Position + angeledVector * distance))
                    {
                        if (so is Grass)
                            detected = true;
                        else
                            detected = false;
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
            return "Grass detector " + (int)(angle * 180 / Math.PI);
        }
    }
}
