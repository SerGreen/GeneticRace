using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticRace.BotNamespace.Sensors;
using GeneticRace.BotNamespace.Sensors.MaterialDetectors;
using GeneticRace.BotClasses.Rules;
using GeneticRace.BotClasses.Sensors;
using GeneticRace.BotClasses.Sensors.MaterialDetectors;

namespace GeneticRace.BotClasses
{
    public class ChromosomeGenerator
    {
        Random rnd;
        public List<CarSensor> sensors;
        List<float> maxSensorValues;
        List<string> actions;
        List<Conjunction> conjunctions;

        public ChromosomeGenerator()
        {
            initActionsList();
            initConjunctions();
            rnd = new Random();
        }

        public ChromosomeGenerator(int seed)
        {
            initActionsList();
            initConjunctions();
            rnd = new Random(seed);
        }

        private void initSensors(Car car, Map map, ref List<Car> cars)
        {
            sensors = new List<CarSensor>();
            maxSensorValues = new List<float>();

            maxSensorValues.Add(6);
            sensors.Add(new Speedometer(car));

            maxSensorValues.Add((float)(360 * Math.PI / 180));
            sensors.Add(new Compass(car));

            maxSensorValues.Add(300);
            sensors.Add(new GrassDetector(car, 0, maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new GrassDetector(car, (float)(-60 * Math.PI / 180), maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new GrassDetector(car, (float)(-90 * Math.PI / 180), maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new GrassDetector(car, (float)(60 * Math.PI / 180), maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new GrassDetector(car, (float)(90 * Math.PI / 180), maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new RoadDetector(car, 0, maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new RoadDetector(car, (float)(-90 * Math.PI / 180), maxSensorValues.Last(), map.SurfaceObjects));

            maxSensorValues.Add(300);
            sensors.Add(new RoadDetector(car, (float)(90 * Math.PI / 180), maxSensorValues.Last(), map.SurfaceObjects));

            /*
            maxSensorValues.Add(300);
            sensors.Add(new CarDetector(car, 0, maxSensorValues.Last(), ref cars));

            maxSensorValues.Add(300);
            sensors.Add(new CarDetector(car, (float)(-60 * Math.PI / 180), maxSensorValues.Last(), ref cars));

            maxSensorValues.Add(300);
            sensors.Add(new CarDetector(car, (float)(-60 * Math.PI / 180), maxSensorValues.Last(), ref cars));
            */
        }

        private void initActionsList()
        {
            actions = new List<string>();
            actions.Add("Accelerate");
            actions.Add("Break");
            actions.Add("SteerLeft");
            actions.Add("SteerRight");
        }

        private void initConjunctions()
        {
            conjunctions = new List<Conjunction>();
            conjunctions.Add(new AND());
            conjunctions.Add(new OR());
            conjunctions.Add(new XOR());
        }

        public Chromosome generateRandomChromosome(Car car, Map map)
        {
            int rulesAmount = rnd.Next(3, 7);   //new random bots start with 3 to 7 rules
            List<float> chrom = generateRandomChromosome(car, map, rulesAmount * 8);
            return new Chromosome(chrom);
        }

        public Chromosome addOneRandomRule(Chromosome chr)
        {
            List<float> newChrom = chr.chromosome;
            for (int i = 0; i < 8; i++)
            {
                newChrom.Add((float)rnd.NextDouble());
            }

            return new Chromosome(newChrom);
        }

        private List<float> generateRandomChromosome(Car car, Map map, int chromosomeLength)
        {
            List<float> chromosome = new List<float>();

            for (int i = 0; i < chromosomeLength; i++)
                chromosome.Add((float)rnd.NextDouble());

            return chromosome;
        }

        public Chromosome mutate(Chromosome chrom, Random rnd)
        {
            Chromosome kar = new Chromosome(chrom.chromosome);
            kar = mutateChromosome(chrom, rnd);
            return kar;
        }

        public Chromosome mutateOneRandmRule(Chromosome chrom, Random rnd)
        {
            int rulesAmount = chrom.chromosome.Count / 8;
            int ruleToMutate = rnd.Next(0, rulesAmount);
            float mutationChance = 0.4f;

            List<float> mChr = new List<float>();

            for (int i = 0; i < chrom.chromosome.Count; i++)
            {
                if (i >= ruleToMutate * 8 && i < ruleToMutate * 8 + 8 && rnd.NextDouble() < mutationChance)
                {
                    mChr.Add((float)rnd.NextDouble());
                    continue;
                }
                else
                    mChr.Add(chrom.chromosome[i]);
            }

            return new Chromosome(mChr);
        }

        public Chromosome mutateChromosome(Chromosome chrom, Random rnd)
        {
            List<float> mChr = new List<float>();
            float mutationChance = 0.4f;

            for (int i = 0; i < chrom.chromosome.Count; i++)
            {
                if (rnd.NextDouble() < mutationChance)
                    mChr.Add((float)rnd.NextDouble());
                else
                    mChr.Add(chrom.chromosome[i]);
            }

            return new Chromosome(mChr);
        }

        public Chromosome crossover(Chromosome chrA, Chromosome chrB, Random rnd)
        {
            int chrALen = chrA.chromosome.Count;
            int chrBLen = chrB.chromosome.Count;

            int gensA = (int)(rnd.NextDouble() * chrALen);
            int gensB = (int)(rnd.Next(0, chrBLen / 8) * 8 + gensA);

            List<float> chr = new List<float>();
            for (int i = 0; i < chrALen - gensA; i++)
                chr.Add(chrA.chromosome[i]);

            for (int i = gensB; i < chrBLen; i++)
                chr.Add(chrB.chromosome[i]);

            Chromosome newChrom = new Chromosome(chr);
            return newChrom;
        }

        public List<Rule> generateRulesList(Chromosome genes, Car car, Map map, ref List<Car> cars)
        {
            initSensors(car, map, ref cars);
            List<Rule> rules = new List<Rule>();
            int rulesAmount = genes.chromosome.Count / 8;

            for (int i = 0; i < rulesAmount; i++)
            {
                Condition first = null;
                Condition second = null;
                Conjunction conj;
                string action;

                switch ((int)(genes.chromosome[i * 8] * 3))
                {
                    case 0:
                        first = new ConditionAlwaysTrue();
                        break;

                    case 1:
                        int sensorIndex = (int)(genes.chromosome[i * 8 + 1] * sensors.Count);
                        CarSensor sens = sensors[sensorIndex];
                        float treshold = maxSensorValues[sensorIndex] * genes.chromosome[i * 8 + 2];
                        first = new ConditionLessThan(sens, treshold);
                        break;

                    case 2:
                        sensorIndex = (int)(genes.chromosome[i * 8 + 1] * sensors.Count);
                        sens = sensors[sensorIndex];
                        treshold = maxSensorValues[sensorIndex] * genes.chromosome[i * 8 + 2];
                        first = new ConditionMoreThan(sens, treshold);
                        break;
                }

                switch ((int)(genes.chromosome[i * 8 + 3] * 3))
                {
                    case 0:
                        second = new ConditionAlwaysTrue();
                        break;

                    case 1:
                        int sensorIndex = (int)(genes.chromosome[i * 8 + 4] * sensors.Count);
                        CarSensor sens = sensors[sensorIndex];
                        float treshold = maxSensorValues[sensorIndex] * genes.chromosome[i * 8 + 5];
                        second = new ConditionLessThan(sens, treshold);
                        break;

                    case 2:
                        sensorIndex = (int)(genes.chromosome[i * 8 + 4] * sensors.Count);
                        sens = sensors[sensorIndex];
                        treshold = maxSensorValues[sensorIndex] * genes.chromosome[i * 8 + 5];
                        second = new ConditionMoreThan(sens, treshold);
                        break;
                }

                conj = conjunctions[(int)(genes.chromosome[i * 8 + 6] * conjunctions.Count)];
                action = actions[(int)(genes.chromosome[i * 8 + 7] * actions.Count)];

                rules.Add(new Rule(first, second, conj, action));
            }

            return rules;
        }
    }
}
