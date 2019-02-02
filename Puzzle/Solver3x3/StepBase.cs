using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class StepBase
    {
        protected StepBase(Puzzle startPuzzle, Goal startGoal)
        {
            this.StartPuzzle = startPuzzle;
            this.StartGoal = startGoal;
        }

        public Goal StartGoal { get; }

        public Puzzle StartPuzzle { get; }

        public abstract IEnumerable<StepInformation> GetPossibleSteps();
    }
}