using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Goal
    {
        private readonly List<(Location Location, PuzzleColor Color)> colors = new List<(Location, PuzzleColor)>();

        public Goal(Goal parent)
        {
            this.Parent = parent;
        }

        public Goal Parent { get; }

        public void AddColor(Location location, PuzzleColor color)
            => this.colors.Add((location, color));

        public bool Check(Puzzle puzzle)
        {
            if ((this.Parent != null) && !this.Parent.Check(puzzle))
            {
                return false;
            }

            foreach (var info in this.colors)
            {
                if (puzzle[info.Location] != info.Color)
                {
                    return false;
                }
            }

            return true;
        }
    }
}