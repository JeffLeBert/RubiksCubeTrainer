using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Step : IStep
    {
        public Step(
            string name,
            Func<Puzzle, bool> initialState,
            Func<Puzzle, bool> finishedState,
            ImmutableArray<AlgorithmCollection> algorithmCollections)
        {
            this.Name = name;
            this.InitialState = initialState;
            this.FinishedState = finishedState;
            this.AlgorithmCollections = algorithmCollections;
        }

        public ImmutableArray<AlgorithmCollection> AlgorithmCollections { get; }

        public Func<Puzzle, bool> FinishedState { get; }

        public Func<Puzzle, bool> InitialState { get; }

        public string Name { get; }

        public IEnumerable<AlgorithmCollection> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmCollection in this.AlgorithmCollections
               where algorithmCollection.InitialState(puzzle)
               select algorithmCollection;

        public bool ShouldUse(Puzzle puzzle)
            => this.InitialState(puzzle);
    }
}