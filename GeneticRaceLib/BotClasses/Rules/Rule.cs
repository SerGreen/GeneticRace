using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRace.BotClasses.Rules
{
    public class Rule
    {
        public Condition First { get; set; }
        public Condition Second { get; set; }
        public Conjunction Conjunction { get; set; }
        public string Action { get; set; }

        public Rule(Condition first, Condition second, Conjunction conjunction, string action)
        {
            this.First = first;
            this.Second = second;
            this.Conjunction = conjunction;
            this.Action = action;
        }

        public bool checkRule()
        {
            return Conjunction.outcome(First, Second);
        }

        public override string ToString()
        {
            return "IF " + First.ToString() + " " + Conjunction.ToString() + " " + Second.ToString() + " THEN " + Action;
        }
    }
}
