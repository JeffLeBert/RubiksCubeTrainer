using System;
using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Algorithm
    {
        public Algorithm(
            string description,
            Func<Puzzle, bool> initialPosition,
            params NotationMoveType[] moves)
        {
            this.Description = description;
            this.InitialPosition = initialPosition;
            this.Moves = moves;
        }

        public string Description { get; }

        public Func<Puzzle, bool> InitialPosition { get; }

        public IEnumerable<NotationMoveType> Moves { get; }
    }
}