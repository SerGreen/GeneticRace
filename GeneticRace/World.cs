using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using GeneticRace.Surface.GrassSurface;
using GeneticRace.Surface.RoadSurface;
using System.Drawing;
using GeneticRace.BotNamespace.Sensors.MaterialDetectors;
using GeneticRace.BotNamespace.Sensors;
using GeneticRace.BotClasses.Rules;
using GeneticRace.BotClasses;
using GeneticRace.BotClasses.Sensors;
using System.IO;

namespace GeneticRace
{
    public class World
    {
        Vector2F camera;

        Random rnd;
        public ChromosomeGenerator chromGen;
        public Map Map { get; set; }
        public List<Car> Cars;
        public List<Bot> Bots { get; set; }
        public List<int> BotsCP { get; set; }   //list of number of last passed check point for each bot
        public int generationNumber;
        int maxGenerationTime;                  //maximum time for evaluate population
        public int generationTime;              //time passed in current generation
        int idleKickTime;                       //time after which bot will be kicked if idle
        int carsAmount;                         //variable for giving IDs for cars, increases every time car added
        public int populationSize { get; set; }
        public int carToFollow { get; set; }    //number of car to follow
        public bool lockBots;                   //this is for avoiding crashes

        Pen penYellow;

        public World(Map map, Vector2F camera)
        {
            this.camera = camera;
            rnd = new Random();
            chromGen = new ChromosomeGenerator();
            maxGenerationTime = int.MaxValue;
            idleKickTime = 300;
            populationSize = 20;
            carToFollow = 0;
            lockBots = false;
            Map = map;

            resetGeneration();
            penYellow = new Pen(Color.Yellow);
        }

        public void resetGeneration()
        {
            lockBots = false;
            carsAmount = 0;
            generationNumber = 0;
            generationTime = 0;

            Cars = new List<Car>();
            Bots = new List<Bot>();
            BotsCP = new List<int>();

            for (int i = 0; i < populationSize; i++)
            {
                Car car = addCar(Map.StartPoint.X, Map.StartPoint.Y);
                Bots.Add(initRandomBot(car));
                BotsCP.Add(-1);
            }
        }

        public List<Bot> getBots()
        { return Bots; }

        public List<int> getCP()
        { return BotsCP; }

        public void setNewCameraSize(float width, float height)
        {
            camera = new Vector2F(width, height);
        }

        public void setNewMap(Map newMap)
        {
            Map = newMap;
            resetGeneration();
        }

        public void render(Graphics g)
        {
            int indexToFollow = carToFollow < Cars.Count ? carToFollow : 0;
            g.TranslateTransform(-(Cars[indexToFollow].Position.X - camera.X / 2), -(Cars[indexToFollow].Position.Y - camera.Y / 2));
            Map.paint(g);

            for (int i=0; i<Cars.Count; i++)
                Cars[i].paint(g);

            foreach (Polygon cp in Map.CheckPoints)
                g.DrawPolygon(penYellow, cp.getPointsArray());
        }

        public void tick()
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                Bot bot = Bots[i];
                List<string> actions = bot.makeDecision();
                foreach (string action in actions)
                    doActionForCar(action, bot.Car.ID);

                if (!bot.GoalAchieved)
                    bot.Time++;
            }

            moveCars();

            evaluateBotsTick();

            bool kik = isAllKicked();
            if (generationTime > maxGenerationTime || kik)
            {
                generationNumber++;
                generationTime = 0;
                Bots = nextGeneration();
            }
            else
                generationTime++;
        }

        private void moveCars()
        {
            for (int i = 0; i < Cars.Count; i++)
                Cars[i].tick(Map.SurfaceObjects, Cars);
        }

        private bool isAllKicked()
        {
            for (int i = 0; i < Bots.Count; i++)
                if (!Bots[i].Kicked && !Bots[i].GoalAchieved)
                    return false;

            return true;
        }

        public void doActionForCar(string action, int id)
        {
            Car car = null;
            for (int i = 0; i < Cars.Count; i++)
                if (Cars[i].ID == id)
                {
                    car = Cars[i];
                    break;
                }

            if (car != null)
            {
                switch (action)
                {
                    case "UpPress":
                        car.startAcceleration();
                        break;

                    case "UpRelease":
                        car.stopAcceleration();
                        break;

                    case "DownPress":
                        car.startBreaking();
                        break;
                    case "DownRelease":
                        car.stopBreaking();
                        break;

                    case "LeftPress":
                        car.startSteerLeft();
                        break;
                    case "LeftRelease":
                        car.stopSteerLeft();
                        break;

                    case "RightPress":
                        car.startSteerRight();
                        break;
                    case "RightRelease":
                        car.stopSteerRight();
                        break;

                    //---------------------------

                    case "Accelerate":
                        car.accelerate();
                        break;

                    case "Break":
                        car.breakSpeed();
                        break;

                    case "SteerRight":
                        car.steerRight(Cars);
                        break;

                    case "SteerLeft":
                        car.steerLeft(Cars);
                        break;
                }
            }
        }

        private Bot initRandomBot(Car car)
        {
            Chromosome karyotype = chromGen.generateRandomChromosome(car, Map);
            List<Rule> rules = chromGen.generateRulesList(karyotype, car, Map, ref Cars);            
            Bot bot = new Bot(car, chromGen.sensors, rules);
            bot.chromosome = karyotype;
            return bot;
        }

        private Bot mutateBot(Bot bot)
        {
            int rulesToMutate = rnd.Next(1, Math.Min(4, bot.rules.Count));
            Car car = addCar(Map.StartPoint.X, Map.StartPoint.Y);
            Chromosome mKar = bot.chromosome;
            for (int i = 0; i < rulesToMutate; i++)
                mKar = chromGen.mutateOneRandmRule(mKar, rnd);
            List<Rule> rules = chromGen.generateRulesList(mKar, car, Map, ref Cars);
            List<CarSensor> sensors = bot.Sensors;

            Bot mutant = new Bot(car, sensors, rules);
            mutant.chromosome = mKar;
            return mutant;
        }

        private Bot crossoverBot(Bot acceptor, Bot donor)
        {
            Car car = addCar(Map.StartPoint.X, Map.StartPoint.Y);
            Chromosome mKar = chromGen.crossover(acceptor.chromosome, donor.chromosome, rnd);
            List<Rule> rules = chromGen.generateRulesList(mKar, car, Map, ref Cars);
            List<CarSensor> sensors = acceptor.Sensors;

            Bot mutant = new Bot(car, sensors, rules);
            mutant.chromosome = mKar;
            return mutant;
        }

        private Bot crossoverMutateBot(Bot acceptor, Bot donor)
        {
            int rulesToMutate = rnd.Next(1, Math.Min(4, acceptor.rules.Count));
            Car car = addCar(Map.StartPoint.X, Map.StartPoint.Y);
            Chromosome mKar = chromGen.crossover(acceptor.chromosome, donor.chromosome, rnd);
            for (int i = 0; i < rulesToMutate; i++)
                mKar = chromGen.mutateOneRandmRule(mKar, rnd);
            List<Rule> rules = chromGen.generateRulesList(mKar, car, Map, ref Cars);
            List<CarSensor> sensors = acceptor.Sensors;

            Bot mutant = new Bot(car, sensors, rules);
            mutant.chromosome = mKar;
            return mutant;
        }

        private List<Bot> nextGeneration()
        {
            BotsCP = new List<int>();
            List<Bot> nextGen = new List<Bot>();
            
            sortBotsByHappiness();
            removeIdenticalBots();

            Bot bestOne = null;
            while (bestOne == null)
            {
                try
                { bestOne = Bots[0]; }
                catch (ArgumentOutOfRangeException)
                { }
            }

            int generationAmount = Bots.Count;

            int mutationsOfBestOne = (generationAmount - 1) / 3;
            int crossoverOfBestOne = (generationAmount - 1 - mutationsOfBestOne) / 2;
            int crossoverMutationOfBestOne = generationAmount - 1 - mutationsOfBestOne - crossoverOfBestOne;
            mutationsOfBestOne += populationSize - generationAmount;

            for (int i = Cars.Count - 1; i >= 0; i--)
            {
                if (Cars[i] != bestOne.Car)
                    Cars.RemoveAt(i);
            }

            nextGen.Add(bestOne);
            bestOne.respawn(Map.StartPoint);
            BotsCP.Add(-1);

            for (int i = 0; i < mutationsOfBestOne; i++)
            {
                nextGen.Add(mutateBot(bestOne));
                BotsCP.Add(-1);
            }

            for (int i = 0; i < crossoverOfBestOne; i++)
            {
                nextGen.Add(crossoverBot(bestOne, Bots[1 + i]));
                BotsCP.Add(-1);
            }
            
            for (int i = 0; i < crossoverMutationOfBestOne; i++)
            {
                nextGen.Add(crossoverMutateBot(bestOne, Bots[1 + i]));
                BotsCP.Add(-1);
            }

            return nextGen;
        }

        private void sortBotsByHappiness()
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;

                for (int i = 0; i < Bots.Count - 1; i++)
                {
                    if (Bots[i].Happiness < Bots[i + 1].Happiness)
                    {
                        Bot temp = Bots[i];
                        Bots[i] = Bots[i + 1];
                        Bots[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
        }

        private void removeIdenticalBots()
        {
            lockBots = true;

            for (int i = 1;;)
            {
                if (i >= Bots.Count)
                    break;

                if (Bots[i].Happiness == Bots[i - 1].Happiness)
                    Bots.RemoveAt(i);
                else
                    i++;
            }

            lockBots = false;
        }

        private Car addCar(float x, float y)
        {
            //classic color of car
            Car car = new Car(x, y, carsAmount, Color.FromArgb(255, 80, 0), Color.Black);
            //random color of car
            //Car car = new Car(x, y, carsAmount, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            Cars.Add(car);
            carsAmount++;
            return car;
        }

        private void evaluateBotsTick()
        {
            for (int j = 0; j < Bots.Count; j++)
            {
                Bot bot = Bots[j];
                if (!bot.GoalAchieved && !bot.Kicked)
                {
                    evaluateBotBasedOnPosition(j, bot);
                }
            }
        }

        private void evaluateBotBasedOnPosition(int j, Bot bot)
        {
            float prevHappiness = bot.Happiness;

            for (int i = 0; i < Map.CheckPoints.Count; i++)
            {
                if (bot.Car.collidesWith(Map.CheckPoints[i]))
                {
                    if (i - 1 == BotsCP[j])
                    {
                        BotsCP[j]++;
                        bot.Happiness += 300;
                        bot.Happiness -= bot.IdleTime;
                        bot.IdleTime = 0;

                        if (BotsCP[j] >= Map.CheckPoints.Count - 1)
                        {
                            bot.Happiness += 200;
                            bot.GoalAchieved = true;
                        }
                    }
                }
            }

            if (bot.Happiness - prevHappiness < 1)
                bot.IdleTime++;
            
            if (bot.IdleTime > idleKickTime)
            {
                bot.Kicked = true;

                Vector2F cpPrev = BotsCP[j] < 0 ? Map.StartPoint : (Vector2F)Map.CheckPoints[BotsCP[j]].Points[0];
                Vector2F cpNext = (Vector2F)Map.CheckPoints[BotsCP[j] + 1].Points[0];

                bot.Happiness += (bot.Car.Position - cpPrev).getLength() / 10;
                bot.Happiness -= (bot.Car.Position - cpNext).getLength() / 10;
            }
        }

        private void evaluateBotBasedOnSurface(int j, Bot bot)
        {
            SurfaceObject topLayerSurface = null;
            foreach (SurfaceObject so in Map.SurfaceObjects)
                if (bot.Car.collidesWith(so.Shape))
                    topLayerSurface = so;

            float speed = bot.Car.SpeedVector.getLength();
            if (topLayerSurface is Road)
                bot.Happiness += speed / 2 < 0.5f ? speed / 2 : 0.5f;
            else
                bot.Happiness -= speed < 3 ? speed : 3;

            for (int i = 0; i < Map.CheckPoints.Count; i++)
            {
                if (bot.Car.collidesWith(Map.CheckPoints[i]))
                {
                    if (i - 1 == BotsCP[j])
                    {
                        BotsCP[j]++;
                        bot.Happiness += 200;
                        bot.IdleTime = 0;

                        if (BotsCP[j] >= Map.CheckPoints.Count - 1)
                        {
                            bot.Happiness += 100;
                            bot.GoalAchieved = true;
                        }
                    }
                }
            }

            bot.IdleTime++;

            if (bot.IdleTime > idleKickTime)
                bot.Kicked = true;
        }

        public void saveGeneration(int slot)
        {
            if (File.Exists("./population_" + slot + ".gen"))
                File.Delete("./population_" + slot + ".gen");

            StreamWriter sr = null;
            string file = "";

            for (int i = 0; i < Bots.Count; i++)
            {
                if (i > 0)
                    file = file + "\n";

                for (int j = 0; j < Bots[i].chromosome.chromosome.Count; j++)
                {
                    if (j > 0)
                        file = file + ":";

                    file = file + Bots[i].chromosome.chromosome[j];
                }
            }

            sr = new StreamWriter("./population_" + slot + ".gen");
            sr.Write(file);
            sr.Close();
        }

        public void loadGeneration(int slot)
        {
            if (File.Exists("./population_" + slot + ".gen"))
            {
                StreamReader sr = new StreamReader("./population_" + slot + ".gen");
                string file = sr.ReadToEnd();
                sr.Close();

                Cars = new List<Car>();
                Bots = new List<Bot>();
                BotsCP = new List<int>();

                string[] botParts = file.Split('\n');

                carsAmount = 0;
                generationNumber = 0;
                for (int i = 0; i < botParts.Length; i++)
                {
                    string[] chrParts = botParts[i].Split(':');

                    List<float> chr = new List<float>();
                    for (int j = 0; j < chrParts.Length; j++)
                        chr.Add(float.Parse(chrParts[j]));

                    Chromosome kar = new Chromosome(chr);
                    Car car = addCar(Map.StartPoint.X, Map.StartPoint.Y);
                    carsAmount = car.ID + 1;
                    List<Rule> rules = chromGen.generateRulesList(kar, car, Map, ref Cars);
                    List<CarSensor> sensors = chromGen.sensors;

                    Bot bot = new Bot(car, sensors, rules);
                    bot.chromosome = kar;
                    Bots.Add(bot);
                    BotsCP.Add(-1);
                }
            }
        }
    }
}
