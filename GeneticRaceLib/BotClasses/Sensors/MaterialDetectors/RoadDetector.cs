using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GeneticRace.BotNamespace.Sensors.MaterialDetectors
{
    public class RoadDetector : MaterialDetector
    {
        public RoadDetector(Car car, float angle, float maxDistance, ArrayList surfaceObjects)
        {
            this.car = car;
            this.angle = angle;
            this.maxDistance = maxDistance;
            this.surfaceObjects = surfaceObjects;
            step = 5;       //acuuracy of measure, step in pixels, less - more acurate but more expensive
        }

        public override float getValue()
        {
            float distance = 0;
            Vector2F angeledVector = car.DirectionVector.rotate(angle);

            while (distance < maxDistance)
            {
                bool detected = false;
                for (int i = 0; i < surfaceObjects.Count; i++)      //searching for the top-layer surface
                {
                    SurfaceObject so = (SurfaceObject)surfaceObjects[i];
                    if (so.Shape.isPointInside(car.Position + angeledVector * distance))
                    {
                        if (so is Road)
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
            return "Road detector " + (int)(angle * 180 / Math.PI);
        }
    }
}
