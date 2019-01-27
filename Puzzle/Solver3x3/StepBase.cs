using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class StepBase
    {
        protected StepBase(Puzzle puzzle)
        {
            this.Puzzle = puzzle;
        }

        public Puzzle Puzzle { get; }

        public abstract IEnumerable<Algorithm> GetPossibleAlgorithms();
    }
}