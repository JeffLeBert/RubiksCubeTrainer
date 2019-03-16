using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    [System.Diagnostics.DebuggerDisplay("Name = {Name}")]
    public class Step
    {
        public Step(
            string name,
            IChecker initialState,
            IChecker finishedState,
            ImmutableArray<Algorithm> algorithms)
        {
            this.Name = name;
            this.InitialState = initialState;
            this.FinishedState = finishedState;
            this.Algorithms = algorithms;
        }

        public ImmutableArray<Algorithm> Algorithms { get; }

        public IChecker FinishedState { get; }

        public IChecker InitialState { get; }

        public string Name { get; }

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithm in this.Algorithms
               where algorithm.InitialState.Matches(puzzle)
               select algorithm;
    }
}