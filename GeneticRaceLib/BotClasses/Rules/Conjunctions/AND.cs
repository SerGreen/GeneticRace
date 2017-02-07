using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotClasses.Rules
{
    public class AND : Conjunction
    {
        public AND()
        { }

        public bool outcome(Condition first, Condition second)
        {
            return first.checkCondition() && second.checkCondition();
        }

        public override string ToString()
        {
            return "AND";
        }
    }
}
