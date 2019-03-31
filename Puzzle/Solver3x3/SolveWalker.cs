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

        public int AlgorithmsTried { get; private set; }

        public int MaximumDepth { get; }

        public Solver Solver { get; }

        public int TotalMoves { get; private set; }

        public List<SolveWalkerState> WalkerStates { get; } = new List<SolveWalkerState>();

        protected bool Visit(Puzzle puzzle)
        {
            if (this.solvedState.Matches(puzzle))
            {
                return this.FoundSolution();
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
            if (this.WalkerStates.Count > this.MaximumDepth)
            {
                this.AtMaximumDepth();
                return false;
            }

            this.WalkerStates.Add(new SolveWalkerState(puzzle, algorithm, moves));
            try
            {
                this.AlgorithmsTried++;
                this.TotalMoves += moves.Length;
                return this.Visit(Rotator.ApplyMoves(puzzle, moves));
            }
            finally
            {
                this.WalkerStates.RemoveAt(this.WalkerStates.Count - 1);
                this.TotalMoves -= moves.Length;
            }
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