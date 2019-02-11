using System;
using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class StepBase
    {
        protected StepBase(Func<Puzzle, bool> endGoal)
        {
            this.EndGoal = endGoal;
            this.AllAlgorithms = this.BuildAllAlgorithms();
        }

        public IEnumerable<AlgorithmInformation> AllAlgorithms { get; }

        public Func<Puzzle, bool> EndGoal { get; }

        public abstract IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle);

        protected abstract AlgorithmInformation[] BuildAllAlgorithms();
    }
}