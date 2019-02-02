﻿using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Goal
    {
        public Goal(Goal parent)
        {
            this.Parent = parent;
        }

        public Goal Parent { get; }

        public IList<CheckerBase> Checkers { get; } = new List<CheckerBase>(); 

        public bool Check(Puzzle puzzle)
        {
            foreach (var checker in this.Checkers)
            {
                if (checker.Check(puzzle))
                {
                    return true;
                }
            }

            if ((this.Parent != null) && !this.Parent.Check(puzzle))
            {
                return false;
            }

            return true;
        }
    }
}