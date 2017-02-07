using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticRace.BotNamespace.Sensors;

namespace GeneticRace.BotClasses.Rules
{
    public class ConditionAlwaysTrue : Condition
    {
        public ConditionAlwaysTrue()
            : base(null, 0)
        { }

        public override bool checkCondition()
        {
            return true;
        }

        public override string ToString()
        {
            return "True";
        }
    }
}
