using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        private Solver(
            ImmutableDictionary<string, IState> states,
            ImmutableDictionary<string, Algorithm> algorithms,
            ImmutableDictionary<string, Algorithm> algorithmTemplates)
        {
            this.States = states;
            this.Algorithms = algorithms;
            this.AlgorithmTemplates = algorithmTemplates;
        }

        public static Solver Empty { get; } = new Solver(
            ImmutableDictionary.Create<string, IState>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, Algorithm>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, Algorithm>(StringComparer.OrdinalIgnoreCase));

        public ImmutableDictionary<string, Algorithm> Algorithms { get; }

        public ImmutableDictionary<string, Algorithm> AlgorithmTemplates { get; }

        public ImmutableDictionary<string, IState> States { get; }

        [DebuggerStepThrough]
        public IEnumerable<Algorithm> PossibleAlgorithms(Puzzle puzzle)
            => from algorithm in this.Algorithms.Values
               where algorithm.InitialState.Matches(puzzle)
               select algorithm;

        public Solver WithAlgorithm(Algorithm algorithm)
            => new Solver(this.States, this.Algorithms.Add(algorithm.Name, algorithm), this.AlgorithmTemplates);

        public Solver WithAlgorithmTemplate(Algorithm algorithm)
            => new Solver(this.States, this.Algorithms, this.AlgorithmTemplates.Add(algorithm.Name, algorithm));

        public Solver WithState(string name, IState state)
            => new Solver(this.States.Add(name, state), this.Algorithms, this.AlgorithmTemplates);
    }
}