using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        private Solver(
            ImmutableDictionary<string, Step> steps,
            ImmutableDictionary<string, IChecker> states,
            ImmutableDictionary<string, Algorithm> algorithms)
        {
            this.Steps = steps;
            this.States = states;
            this.Algorithms = algorithms;
        }

        public static Solver Empty { get; } = new Solver(
            ImmutableDictionary.Create<string, Step>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, IChecker>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, Algorithm>(StringComparer.OrdinalIgnoreCase));

        public ImmutableDictionary<string, Algorithm> Algorithms { get; }

        public ImmutableDictionary<string, IChecker> States { get; }

        public ImmutableDictionary<string, Step> Steps { get; }

        public IEnumerable<Step> NextSteps(Puzzle puzzle)
            => from step in this.Steps.Values
               where step.InitialState.Matches(puzzle)
               select step;

        public Solver With(Algorithm algorithm)
            => new Solver(
                this.Steps,
                this.States,
                this.Algorithms.Add(algorithm.Name, algorithm));

        public Solver With(string name, IChecker state)
            => new Solver(
                this.Steps,
                this.States.Add(name, state),
                this.Algorithms);

        public Solver With(Step step)
            => new Solver(
                this.Steps.Add(step.Name, step),
                this.States,
                this.Algorithms);
    }
}