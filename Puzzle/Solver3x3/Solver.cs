using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        private Solver(ImmutableDictionary<string, IState> states, ImmutableDictionary<string, Algorithm> algorithms)
        {
            this.States = states;
            this.Algorithms = algorithms;
        }

        public static Solver Empty { get; } = new Solver(
            ImmutableDictionary.Create<string, IState>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, Algorithm>(StringComparer.OrdinalIgnoreCase));

        public ImmutableDictionary<string, Algorithm> Algorithms { get; }

        public ImmutableDictionary<string, IState> States { get; }

        public IEnumerable<Algorithm> NextAlgorithms(Puzzle puzzle)
            => from algorithm in this.Algorithms.Values
               where algorithm.InitialState.Matches(puzzle)
               select algorithm;

        public Solver With(Algorithm algorithm)
            => new Solver(this.States, this.Algorithms.Add(algorithm.Name, algorithm));

        public Solver With(string name, IState state)
            => new Solver(this.States.Add(name, state), this.Algorithms);
    }
}