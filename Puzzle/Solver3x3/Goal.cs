using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Goal
    {
        public Goal(Goal parent)
        {
            this.Parent = parent;
        }

        public Goal Parent { get; }

        public IList<IChecker> Checkers { get; } = new List<IChecker>(); 

        public bool Check(Puzzle puzzle)
        {
            foreach (var checker in this.Checkers)
            {
                if (!checker.Check(puzzle))
                {
                    return false;
                }
            }

            if ((this.Parent != null) && !this.Parent.Check(puzzle))
            {
                return false;
            }

            return true;
        }
    }
}