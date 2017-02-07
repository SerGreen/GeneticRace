using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using GeneticRace.BaseEntities;

namespace GeneticRace
{
    public class Car : DrawableObject
    {
        public int ID { get; set; }
        public Polygon Body { get; set; }
        public Vector2F Position { get; set; }
        public Vector2F SpeedVector { get; set; }
        public Vector2F DirectionVector { get; set; }
        public float Health { get; set; }   //not used yet
        public bool ignoreCars;
        public bool collideWithCars;

        private bool accelerating;
        private bool breaking;
        private bool steeringLeft;
        private bool steeringRight;

        private Pen penPrimary;
        private Pen penSecond;
        private Brush brush;

        private float maxSpeed = 5;
        private float accelerationFactor = 0.5f;
        private float breakingFactor = 0.3f;
        private float steeringFactor = (float)(5 * Math.PI / 180);

        public Car(float x, float y, int id, Color primeColor, Color secondColor)
        {
            ID = id;

            respawn(new Vector2F(x, y));

            collideWithCars = false;
            brush = new SolidBrush(primeColor);
            penPrimary = new Pen(primeColor, 3);
            penSecond = new Pen(secondColor, 2);
        }

        public void respawn(Vector2F position)
        {
            int halfWidth = 10;     //in pixels
            int halfHeight = 20;
            ArrayList bodyPoints = new ArrayList();                     //forming body of car
            bodyPoints.Add(new Vector2F(-halfWidth, -halfHeight));
            bodyPoints.Add(new Vector2F(0, -halfHeight));               //point is needed for black arrow image on car
            bodyPoints.Add(new Vector2F(halfWidth, -halfHeight));
            bodyPoints.Add(new Vector2F(halfWidth, -halfHeight / 2));   //same
            bodyPoints.Add(new Vector2F(halfWidth, halfHeight));
            bodyPoints.Add(new Vector2F(-halfWidth, halfHeight));
            bodyPoints.Add(new Vector2F(-halfWidth, -halfHeight / 2));  //same

            Body = new Polygon(bodyPoints);
            Position = position;
            SpeedVector = new Vector2F(0, 0);
            DirectionVector = new Vector2F(0, -1);
            Health = 100;
            ignoreCars = true;

            accelerating = false;
            breaking = false;
            steeringLeft = false;
            steeringRight = false;
        }

        public void tick(ArrayList surfaceObjects, List<Car> cars)
        {
            bool canMove = true;
            for (int i = 0; i < cars.Count; i++)
                if (cars[i] != this && collidesWith(cars[i].Body + cars[i].Position, SpeedVector))
                {
                    canMove = false;
                    break;
                }

            if (canMove || ignoreCars)
                Position += SpeedVector;
            else
                SpeedVector = new Vector2F(0, 0);

            if (canMove && collideWithCars && ignoreCars)
                ignoreCars = false;

            float friction = 0;
            foreach (SurfaceObject so in surfaceObjects)
                if (collidesWith(so.Shape))
                    friction = so.Friction;

            SpeedVector *= friction;

            if (accelerating)
                accelerate();

            if (breaking)
                breakSpeed();

            if (steeringLeft)
                steerLeft();

            if (steeringRight)
                steerRight();
        }

        public bool collidesWith(Shape shape)
        {
            return shape.collidesWith(Body + Position);
        }

        public bool collidesWith(Shape shape, Vector2F deltaVector)
        {
            return shape.collidesWith(Body + (Position + deltaVector));
        }

        public bool collidesWith(Shape shape, Vector2F deltaVector, float deltaAngle)
        {
            return shape.collidesWith(Body.rotateAround(new Vector2F(0, 0), deltaAngle) + (Position + deltaVector));
        }

        public void startAcceleration()
        { accelerating = true; }

        public void stopAcceleration()
        { accelerating = false; }

        public void startBreaking()
        { breaking = true; }

        public void stopBreaking()
        { breaking = false; }

        public void startSteerLeft()
        { steeringLeft = true; }

        public void stopSteerLeft()
        { steeringLeft = false; }

        public void startSteerRight()
        { steeringRight = true; }

        public void stopSteerRight()
        { steeringRight = false; }

        public void accelerate()
        {
            if (SpeedVector.getLength() < maxSpeed)
                SpeedVector += DirectionVector * accelerationFactor;
        }

        public void breakSpeed()
        {
            if (SpeedVector.getLength() < maxSpeed)
                SpeedVector -= DirectionVector * breakingFactor;
        }

        public void steerLeft()
        {
            float steerDegree = SpeedVector.getLength() * steeringFactor;
            if (steerDegree > steeringFactor)
                steerDegree = steeringFactor;

            DirectionVector = DirectionVector.rotate(-steerDegree);
            SpeedVector = SpeedVector.rotate(-steerDegree);
            Body = Body.rotateAround(new Vector2F(0, 0), -steerDegree);
        }

        public void steerLeft(List<Car> cars)
        {
            bool canSteer = true;
            if (!ignoreCars)
            {
                foreach (Car c in cars)
                    if (c != this && collidesWith(c.Body + c.Position, SpeedVector, -steeringFactor))
                    {
                        canSteer = false;
                        break;
                    }
            }

            if (canSteer)
                steerLeft();
        }

        public void steerRight()
        {
            float steerDegree = SpeedVector.getLength() * steeringFactor;
            if (steerDegree > steeringFactor)
                steerDegree = steeringFactor;

            DirectionVector = DirectionVector.rotate(steerDegree);
            SpeedVector = SpeedVector.rotate(steerDegree);
            Body = Body.rotateAround(new Vector2F(0, 0), steerDegree);
        }

        public void steerRight(List<Car> cars)
        {
            bool canSteer = true;
            if (!ignoreCars)
            {
                foreach (Car c in cars)
                    if (c != this && collidesWith(c.Body + c.Position, SpeedVector, steeringFactor))
                    {
                        canSteer = false;
                        break;
                    }
            }

            if (canSteer)
                steerRight();
        }

        public void paint(System.Drawing.Graphics g)
        {
            g.DrawLines(penPrimary, new Point[] { (Vector2F)Body.Points[0] + Position, 
                                                  (Vector2F)Body.Points[2] + Position,
                                                  (Vector2F)Body.Points[4] + Position,
                                                  (Vector2F)Body.Points[5] + Position,
                                                  (Vector2F)Body.Points[0] + Position });   //body contour
            g.DrawLines(penSecond, new Point[] { (Vector2F)Body.Points[6] + Position,
                                                 (Vector2F)Body.Points[1] + Position,
                                                 (Vector2F)Body.Points[3] + Position });    //black arrow
            
            g.DrawLine(penSecond, Position, Position + SpeedVector * 3);
        }

        public void paintOld(System.Drawing.Graphics g)
        {
            for (int i = 0; i < Body.Points.Count; i++)
            {
                Vector2F a = (Vector2F)Body.Points[i];
                Vector2F b = (Vector2F)Body.Points[(i + 1) % Body.Points.Count];

                Pen p = i == 0 ? new Pen(penSecond.Color, 3) : penSecond;
                g.DrawLine(p, a + Position, b + Position);
            }

            g.DrawLine(penSecond, Position, Position + SpeedVector);
        }

        public override string ToString()
        {
            return "ID:" + ID;
        }
    }
}
