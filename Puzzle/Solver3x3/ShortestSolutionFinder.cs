using System.Collections.Generic;
using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class ShortestSolutionFinder : SolveWalker
    {
        private SolveWalkerState[] bestSolution;
        private int bestSolutionLength = int.MaxValue;
        private string message;

        public ShortestSolutionFinder(Solver solver, int maximumDepth = 50)
            : base(solver, maximumDepth)
        {
        }

        public (bool FoundSolution, string Description, IEnumerable<SolveWalkerState> States) FindSolution(Puzzle puzzle)
        {
            this.Visit(puzzle);

            return (this.bestSolutionLength < int.MaxValue, this.message, this.bestSolution);
        }

        protected override void AtMaximumDepth()
        {
            this.message = "Got stuck in a loop somewhere...";
            this.bestSolutionLength = int.MaxValue;
            this.bestSolution = this.WalkerStates.ToArray();
        }

        protected override bool FoundSolution()
        {
            if (this.TotalMoves < this.bestSolutionLength)
            {
                this.message = "Solution found.";
                this.bestSolutionLength = this.TotalMoves;
                this.bestSolution = this.WalkerStates.ToArray();
            }

            return true;
        }

        protected override void NoSolutionsFound()
        {
            this.message = "No solution found.";
            this.bestSolutionLength = int.MaxValue;
            this.bestSolution = this.WalkerStates.ToArray();
        }

        protected override bool Visit(Puzzle puzzle, Algorithm algorithm, ImmutableArray<NotationMoveType> moves)
            => this.TotalMoves + moves.Length > this.bestSolutionLength
            ? true
            : base.Visit(puzzle, algorithm, moves);
    }
}