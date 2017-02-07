using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotClasses.Sensors
{
    public class Chromosome
    {
        public List<float> chromosome { get; set; }

        public Chromosome(List<float> chrom)
        {
            chromosome = chrom;
        }
    }
}
