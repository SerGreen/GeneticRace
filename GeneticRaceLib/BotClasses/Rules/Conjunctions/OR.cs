using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotClasses.Rules
{
    public class OR : Conjunction
    {
        public OR()
        { }

        public bool outcome(Condition first, Condition second)
        {
            return first.checkCondition() || second.checkCondition();
        }

        public override string ToString()
        {
            return "OR";
        }
    }
}
