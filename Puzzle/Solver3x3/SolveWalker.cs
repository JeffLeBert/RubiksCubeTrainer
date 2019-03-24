using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class SolveWalker
    {
        private readonly IState solvedState;

        protected SolveWalker(Solver solver, int maximumDepth = 50)
        {
            this.Solver = solver;
            this.MaximumDepth = maximumDepth;

            this.solvedState = this.Solver.States["Solved"];
        }

        public int Depth { get; private set; }

        public int MaximumDepth { get; }

        public Solver Solver { get; }

        public List<SolveWalkerState> WalkerStates { get; } = new List<SolveWalkerState>();

        protected bool Visit(Puzzle puzzle)
        {
            this.Depth = 0;

            if (this.solvedState.Matches(puzzle))
            {
                this.FoundSolution();
                return true;
            }

            return this.Visit(puzzle, this.Solver.PossibleAlgorithms(puzzle).ToArray());
        }

        protected virtual bool Visit(Puzzle puzzle, Algorithm[] algorithms)
        {
            if (algorithms.Length == 0)
            {
                this.NoSolutionsFound();
                return false;
            }

            foreach (var algorithm in algorithms)
            {
                if (!this.Visit(puzzle, algorithm))
                {
                    return false;
                }
            }

            return true;
        }

        protected virtual bool Visit(Puzzle puzzle, Algorithm algorithm)
        {
            foreach (var move in algorithm.Moves)
            {
                if (!this.Visit(puzzle, algorithm, move))
                {
                    return false;
                }
            }

            return true;
        }

        protected virtual bool Visit(Puzzle puzzle, Algorithm algorithm, ImmutableArray<NotationMoveType> moves)
        {
            this.Depth++;
            if (this.Depth > this.MaximumDepth)
            {
                this.AtMaximumDepth();
                return false;
            }

            this.WalkerStates.Add(new SolveWalkerState(puzzle, algorithm, moves));
            try
            {
                var newPuzzle = Rotator.ApplyMoves(puzzle, moves);
                if (this.solvedState.Matches(newPuzzle))
                {
                    return this.FoundSolution();
                }

                if (!this.Visit(newPuzzle, this.Solver.PossibleAlgorithms(newPuzzle).ToArray()))
                {
                    return false;
                }
            }
            finally
            {
                this.WalkerStates.RemoveAt(this.WalkerStates.Count - 1);
            }

            return false;
        }

        protected virtual void AtMaximumDepth()
        {
        }

        protected virtual bool FoundSolution()
            // Keep looking for more solutions.
            => true;

        protected virtual void NoSolutionsFound()
        {
        }
    }
}