using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveEdgeToFrontBottomStep : StepBase
    {
        public MoveEdgeToFrontBottomStep(PuzzleColor color1, PuzzleColor color2)
            : base(BuildEndGoal(color1, color2))
        {
            this.Color1 = color1;
            this.Color2 = color2;
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

        public override IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.AllAlgorithms
               where algorithmInfo.InitialPosition(puzzle)
               select algorithmInfo;

        protected override AlgorithmInformation[] BuildAllAlgorithms()
            => new AlgorithmInformation[]
            {
                this.MoveEdgeFromFrontLeft(),
                this.MoveEdgeFromFrontUp(),
                this.MoveEdgeFromFrontRight(),
                this.MoveEdgeFromBackLeft(),
                this.MoveEdgeFromBackUp(),
                this.MoveEdgeFromBackRight(),
                this.MoveEdgeFromBackDown(),
                this.MoveEdgeFromRightUp(),
                this.MoveEdgeFromRightDown(),
                this.MoveEdgeFromLeftUp()
            };

        private AlgorithmInformation MoveEdgeFromFrontUp()
            => new AlgorithmInformation(
                new Algorithm("Move edge from front top.", NotationMoveType.FrontDouble),
                Checker.EdgeOrFlipped(Location.FrontUpEdge, PuzzleColor.Red, PuzzleColor.Blue));

        private AlgorithmInformation MoveEdgeFromFrontLeft()
            => new AlgorithmInformation(
                new Algorithm("Move edge from front left.", NotationMoveType.FrontCounterClockwise),
                Checker.EdgeOrFlipped(Location.FrontLeftEdge, PuzzleColor.Red, PuzzleColor.Blue));

        private AlgorithmInformation MoveEdgeFromFrontRight()
            => new AlgorithmInformation(
                new Algorithm("Move edge from front right.", NotationMoveType.FrontClockwise),
                Checker.EdgeOrFlipped(Location.FrontRightEdge, PuzzleColor.Red, PuzzleColor.Blue));

        private AlgorithmInformation MoveEdgeFromBackLeft()
            => new AlgorithmInformation(
                new Algorithm("Move edge from back left.", NotationMoveType.BackClockwise, NotationMoveType.MiddleMCounterClockwise),
                Checker.EdgeOrFlipped(Location.LeftBackEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private AlgorithmInformation MoveEdgeFromBackUp()
            => new AlgorithmInformation(
                new Algorithm("Move edge from back up.", NotationMoveType.MiddleMDouble),
                Checker.EdgeOrFlipped(Location.UpBackEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private AlgorithmInformation MoveEdgeFromBackRight()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back right.",
                    NotationMoveType.BackClockwise,
                    NotationMoveType.UpDouble,
                    NotationMoveType.BackCounterClockwise,
                    NotationMoveType.MiddleMClockwise),
                Checker.EdgeOrFlipped(Location.BackRightEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private AlgorithmInformation MoveEdgeFromBackDown()
            => new AlgorithmInformation(
                new Algorithm("Move edge from back down.", NotationMoveType.MiddleMCounterClockwise),
                Checker.EdgeOrFlipped(Location.BackDownEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private AlgorithmInformation MoveEdgeFromRightUp()
            => new AlgorithmInformation(
                new Algorithm("Move edge from up right.", NotationMoveType.UpClockwise, NotationMoveType.MiddleMClockwise),
                Checker.EdgeOrFlipped(Location.RightUpEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private AlgorithmInformation MoveEdgeFromRightDown()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right down.",
                    NotationMoveType.RightDouble,
                    NotationMoveType.UpClockwise,
                    NotationMoveType.MiddleMClockwise),
                Checker.EdgeOrFlipped(Location.RightDownEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private AlgorithmInformation MoveEdgeFromLeftUp()
            => new AlgorithmInformation(
                new Algorithm("Move edge from right up.", NotationMoveType.UpCounterClockwise, NotationMoveType.MiddleMClockwise),
                Checker.EdgeOrFlipped(Location.LeftUpEdge, PuzzleColor.Blue, PuzzleColor.Red));

        private static Func<Puzzle, bool> BuildEndGoal(PuzzleColor color1, PuzzleColor color2)
            => puzzle => Checker.EdgeOrFlipped(Location.FrontDownEdge, color1, color2)(puzzle)
                && FirstPieceToBottomLeftEdgeStep.Instance.EndGoal(puzzle);
    }
}