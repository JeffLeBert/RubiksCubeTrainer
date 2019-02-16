using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveEdgeToFrontBottomStep : IStep
    {
        private readonly ImmutableArray<Algorithm> allAlgorithms;

        public MoveEdgeToFrontBottomStep(PuzzleColor color1, PuzzleColor color2)
        {
            this.Color1 = color1;
            this.Color2 = color2;
            this.allAlgorithms = this.BuildAllAlgorithms();
        }

        public static MoveEdgeToFrontBottomStep InstanceFrontLeft { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Blue,
            PuzzleColor.Red);

        public static MoveEdgeToFrontBottomStep InstanceBackLeft { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Blue,
            PuzzleColor.Orange);

        public static MoveEdgeToFrontBottomStep InstanceFrontRight { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Green,
            PuzzleColor.Red);

        public static MoveEdgeToFrontBottomStep InstanceBackRight { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Green,
            PuzzleColor.Orange);

        public PuzzleColor Color1 { get; }

        public PuzzleColor Color2 { get; }

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialPosition(puzzle)
               select algorithmInfo;

        private ImmutableArray<Algorithm> BuildAllAlgorithms()
            => ImmutableArray.Create(
                this.MoveEdgeFromFrontLeft(),
                this.MoveEdgeFromFrontUp(),
                this.MoveEdgeFromFrontRight(),
                this.MoveEdgeFromBackLeft(),
                this.MoveEdgeFromBackUp(),
                this.MoveEdgeFromBackRight(),
                this.MoveEdgeFromBackDown(),
                this.MoveEdgeFromRightUp(),
                this.MoveEdgeFromRightDown(),
                this.MoveEdgeFromLeftUp());

        private Algorithm MoveEdgeFromFrontUp()
            => new Algorithm(
                "Move edge from front top.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.FrontUp, PuzzleColor.Red, PuzzleColor.Blue),
                NotationMoveType.FrontDouble);

        private Algorithm MoveEdgeFromFrontLeft()
            => new Algorithm(
                "Move edge from front left.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.FrontLeft, PuzzleColor.Red, PuzzleColor.Blue),
                NotationMoveType.FrontCounterClockwise);

        private Algorithm MoveEdgeFromFrontRight()
            => new Algorithm(
                "Move edge from front right.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.FrontRight, PuzzleColor.Red, PuzzleColor.Blue),
                NotationMoveType.FrontClockwise);

        private Algorithm MoveEdgeFromBackLeft()
            => new Algorithm(
                "Move edge from back left.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.LeftBack, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.BackClockwise,
                NotationMoveType.MiddleMCounterClockwise);

        private Algorithm MoveEdgeFromBackUp()
            => new Algorithm(
                "Move edge from back up.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.UpBack, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.MiddleMDouble);

        private Algorithm MoveEdgeFromBackRight()
            => new Algorithm(
                "Move edge from back right.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.BackRight, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.BackClockwise,
                NotationMoveType.UpDouble,
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.MiddleMClockwise);

        private Algorithm MoveEdgeFromBackDown()
            => new Algorithm(
                "Move edge from back down.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.BackDown, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.MiddleMCounterClockwise);

        private Algorithm MoveEdgeFromRightUp()
            => new Algorithm(
                "Move edge from up right.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.RightUp, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.UpClockwise,
                NotationMoveType.MiddleMClockwise);

        private Algorithm MoveEdgeFromRightDown()
            => new Algorithm(
                "Move edge from right down.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.RightDown, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.RightDouble,
                NotationMoveType.UpClockwise,
                NotationMoveType.MiddleMClockwise);

        private Algorithm MoveEdgeFromLeftUp()
            => new Algorithm(
                "Move edge from right up.",
                puzzle => Checker.EdgeOrFlipped(puzzle, Location.LeftUp, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.MiddleMClockwise);

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.EdgeOrFlipped(puzzle, Location.FrontDown, this.Color1, this.Color2);
    }
}