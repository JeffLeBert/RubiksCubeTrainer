using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Step
    {
        public Step(
            string name,
            IChecker initialState,
            IChecker finishedState,
            ImmutableArray<AlgorithmCollection> algorithmCollections)
        {
            this.Name = name;
            this.InitialState = initialState;
            this.FinishedState = finishedState;
            this.AlgorithmCollections = algorithmCollections;
        }

        public ImmutableArray<AlgorithmCollection> AlgorithmCollections { get; }

        public IChecker FinishedState { get; }

        public IChecker InitialState { get; }

        public string Name { get; }

        public IEnumerable<AlgorithmCollection> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmCollection in this.AlgorithmCollections
               where algorithmCollection.InitialState.Matches(puzzle)
               select algorithmCollection;
    }
}