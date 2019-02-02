using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class StepInformation
    {
        private readonly Func<Puzzle, StepBase> nextStep;

        public StepInformation(
            Algorithm algorithm,
            Func<Puzzle, StepBase> nextStep,
            params CheckerBase[] checkers)
        {
            this.Checkers = checkers;
            this.Algorithm = algorithm;
            this.nextStep = nextStep;
        }

        public Algorithm Algorithm { get; }

        public IEnumerable<CheckerBase> Checkers { get; }

        public bool PassesAllChecks(Puzzle puzzle)
            => this.Checkers.All(checker => checker.Check(puzzle));

        public StepBase NextStep(Puzzle puzzle)
            => this.nextStep(puzzle);
    }
}