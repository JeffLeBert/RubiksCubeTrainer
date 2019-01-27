using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Roux.Solver3x3
{
    public class Solver
    {
        private readonly Puzzle puzzle;

        public Solver(Puzzle puzzle)
        {
            this.puzzle = puzzle;
        }

        public IEnumerable<string> NextMoves()
        {
            return new string[] { };
        }
    }
}