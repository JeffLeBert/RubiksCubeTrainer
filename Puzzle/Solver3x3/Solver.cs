using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        private Solver(ImmutableDictionary<string, Step> steps)
        {
            this.Steps = steps;
        }

        public static Solver Empty { get; } = new Solver(
            ImmutableDictionary<string, Step>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase));

        public Solver WithStep(Step step)
            => new Solver(this.Steps.Add(step.Name, step));

        public ImmutableDictionary<string, Step> Steps { get; }

        public IEnumerable<IStep> NextSteps(Puzzle puzzle)
            => from step in this.Steps.Values
               where !step.FinishedState(puzzle)
               select step;
    }
}