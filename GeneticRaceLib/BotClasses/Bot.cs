using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticRace.BotNamespace.Sensors;
using System.Collections;
using GeneticRace.BotNamespace.Sensors.MaterialDetectors;
using GeneticRace.BotClasses.Rules;
using GeneticRace.BotClasses.Sensors;

namespace GeneticRace
{
    public class Bot
    {
        public Car Car { get; set; }
        public List<CarSensor> Sensors { get; set; }
        public List<Rule> rules { get; set; }
        public float Happiness { get; set; }
        public bool GoalAchieved { get; set; }
        public bool Kicked { get; set; }
        public int IdleTime { get; set; }
        public int Time { get; set; }
        public Chromosome chromosome { get; set; }

        public Bot(Car car, List<CarSensor> sensors, List<Rule> rules)
        {
            this.Car = car;
            this.rules = rules;
            this.Sensors = sensors;
            resetStats();
        }

        public void respawn(Vector2F position)
        {
            Car.respawn(position);
            resetStats();
        }

        private void resetStats()
        {
            Happiness = 0;
            GoalAchieved = false;
            Kicked = false;
            IdleTime = 0;
            Time = 0;
        }

        private void initSensors(ArrayList surfaceObjects)
        {
            Sensors = new List<CarSensor>();

            Sensors.Add(new Speedometer(Car));
            Sensors.Add(new Compass(Car));
            Sensors.Add(new GrassDetector(Car, 0, 300, surfaceObjects));
            Sensors.Add(new GrassDetector(Car, (float)(-60 * Math.PI / 180), 300, surfaceObjects));
            Sensors.Add(new GrassDetector(Car, (float)(60 * Math.PI / 180), 300, surfaceObjects));
            Sensors.Add(new RoadDetector(Car, 0, 300, surfaceObjects));
        }

        public List<string> makeDecision()
        {
            if (!GoalAchieved && !Kicked)
            {
                List<string> actions = new List<string>();

                foreach (Rule rule in rules)
                {
                    if (rule.checkRule() && !actions.Contains(rule.Action))
                        actions.Add(rule.Action);
                }

                return actions;
            }

            return new List<string>() { "NoOp" };
        }

        public override string ToString()
        {
            return "Fitness:" + Happiness;
        }
    }
}
