using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotClasses.Rules
{
    public class XOR : Conjunction
    {
        public XOR()
        { }

        public bool outcome(Condition first, Condition second)
        {
            bool f = first.checkCondition();
            bool s = second.checkCondition();

            return (f && !s) || (s && !f);
        }

        public override string ToString()
        {
            return "XOR";
        }
    }
}
