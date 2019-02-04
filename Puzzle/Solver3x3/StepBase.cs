using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class StepBase
    {
        protected StepBase()
        {
            this.EndGoal = this.BuildEndGoal();
            this.AllAlgorithms = this.BuildAllAlgorithms();
        }

        public IEnumerable<AlgorithmInformation> AllAlgorithms { get; }

        public Goal EndGoal { get; }

        public abstract IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle);

        protected abstract AlgorithmInformation[] BuildAllAlgorithms();

        protected abstract Goal BuildEndGoal();
    }
}