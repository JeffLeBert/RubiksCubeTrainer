using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Algorithm
    {
        public Algorithm(string description, params NotationMoveType[] moves)
        {
            this.Description = description;
            this.Moves = moves;
        }

        public string Description { get; }

        public IEnumerable<NotationMoveType> Moves { get; }
    }
}