using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class SolverBase
    {
        protected SolverBase()
        {
        }

        public abstract IEnumerable<StepBase> NextSteps(Puzzle puzzle);
    }
}