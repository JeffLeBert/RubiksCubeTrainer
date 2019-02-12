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
            this.EndGoal = BuildEndGoal(color1, color2);
            this.allAlgorithms = this.BuildAllAlgorithms();
        }

        public static MoveEdgeToFrontBottomStep InstanceBlueRed { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Blue,
            PuzzleColor.Red);

        public static MoveEdgeToFrontBottomStep InstanceBlueOrange { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Blue,
            PuzzleColor.Orange);

        public static MoveEdgeToFrontBottomStep InstanceGreeRed { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Green,
            PuzzleColor.Red);

        public static MoveEdgeToFrontBottomStep InstanceGreenOrange { get; } = new MoveEdgeToFrontBottomStep(
            PuzzleColor.Green,
            PuzzleColor.Orange);

        public PuzzleColor Color1 { get; }

        public PuzzleColor Color2 { get; }

        public Func<Puzzle, bool> EndGoal { get; }

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
                Checker.EdgeOrFlipped(Location.FrontUpEdge, PuzzleColor.Red, PuzzleColor.Blue),
                NotationMoveType.FrontDouble);

        private Algorithm MoveEdgeFromFrontLeft()
            => new Algorithm(
                "Move edge from front left.",
                Checker.EdgeOrFlipped(Location.FrontLeftEdge, PuzzleColor.Red, PuzzleColor.Blue),
                NotationMoveType.FrontCounterClockwise);

        private Algorithm MoveEdgeFromFrontRight()
            => new Algorithm(
                "Move edge from front right.",
                Checker.EdgeOrFlipped(Location.FrontRightEdge, PuzzleColor.Red, PuzzleColor.Blue),
                NotationMoveType.FrontClockwise);

        private Algorithm MoveEdgeFromBackLeft()
            => new Algorithm(
                "Move edge from back left.",
                Checker.EdgeOrFlipped(Location.LeftBackEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.BackClockwise,
                NotationMoveType.MiddleMCounterClockwise);

        private Algorithm MoveEdgeFromBackUp()
            => new Algorithm(
                "Move edge from back up.",
                Checker.EdgeOrFlipped(Location.UpBackEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.MiddleMDouble);

        private Algorithm MoveEdgeFromBackRight()
            => new Algorithm(
                "Move edge from back right.",
                Checker.EdgeOrFlipped(Location.BackRightEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.BackClockwise,
                NotationMoveType.UpDouble,
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.MiddleMClockwise);

        private Algorithm MoveEdgeFromBackDown()
            => new Algorithm(
                "Move edge from back down.",
                Checker.EdgeOrFlipped(Location.BackDownEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.MiddleMCounterClockwise);

        private Algorithm MoveEdgeFromRightUp()
            => new Algorithm(
                "Move edge from up right.",
                Checker.EdgeOrFlipped(Location.RightUpEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.UpClockwise,
                NotationMoveType.MiddleMClockwise);

        private Algorithm MoveEdgeFromRightDown()
            => new Algorithm(
                "Move edge from right down.",
                Checker.EdgeOrFlipped(Location.RightDownEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.RightDouble,
                NotationMoveType.UpClockwise,
                NotationMoveType.MiddleMClockwise);

        private Algorithm MoveEdgeFromLeftUp()
            => new Algorithm(
                "Move edge from right up.",
                Checker.EdgeOrFlipped(Location.LeftUpEdge, PuzzleColor.Blue, PuzzleColor.Red),
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.MiddleMClockwise);

        private static Func<Puzzle, bool> BuildEndGoal(PuzzleColor color1, PuzzleColor color2)
            => puzzle => Checker.EdgeOrFlipped(Location.FrontDownEdge, color1, color2)(puzzle)
                && FirstPieceToBottomLeftEdgeStep.Instance.EndGoal(puzzle);
    }
}