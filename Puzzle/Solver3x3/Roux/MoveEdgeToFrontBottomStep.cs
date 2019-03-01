using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveEdgeToFrontBottomStep : IStep
    {
        private readonly ImmutableArray<AlgorithmCollection> allAlgorithms;

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

        public string Name => nameof(MoveEdgeToFrontBottomStep);

        public IEnumerable<AlgorithmCollection> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialState.Matches(puzzle)
               select algorithmInfo;

        private ImmutableArray<AlgorithmCollection> BuildAllAlgorithms()
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

        private AlgorithmCollection MoveEdgeFromFrontUp()
            => new AlgorithmCollection(
                "Move edge from front top.",
                new PuzzleState("FrontUp* Red Blue"),
                NotationMoveType.FrontDouble);

        private AlgorithmCollection MoveEdgeFromFrontLeft()
            => new AlgorithmCollection(
                "Move edge from front left.",
                new PuzzleState("FrontLeft* Red Blue"),
                NotationMoveType.FrontCounterClockwise);

        private AlgorithmCollection MoveEdgeFromFrontRight()
            => new AlgorithmCollection(
                "Move edge from front right.",
                new PuzzleState("FrontRight* Red Blue"),
                NotationMoveType.FrontClockwise);

        private AlgorithmCollection MoveEdgeFromBackLeft()
            => new AlgorithmCollection(
                "Move edge from back left.",
                new PuzzleState("LeftBack* Blue Red"),
                NotationMoveType.BackClockwise,
                NotationMoveType.MiddleMCounterClockwise);

        private AlgorithmCollection MoveEdgeFromBackUp()
            => new AlgorithmCollection(
                "Move edge from back up.",
                new PuzzleState("UpBack* Blue Red"),
                NotationMoveType.MiddleMDouble);

        private AlgorithmCollection MoveEdgeFromBackRight()
            => new AlgorithmCollection(
                "Move edge from back right.",
                new PuzzleState("BackRight* Blue Red"),
                NotationMoveType.BackClockwise,
                NotationMoveType.UpDouble,
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.MiddleMClockwise);

        private AlgorithmCollection MoveEdgeFromBackDown()
            => new AlgorithmCollection(
                "Move edge from back down.",
                new PuzzleState("BackDown* Blue Red"),
                NotationMoveType.MiddleMCounterClockwise);

        private AlgorithmCollection MoveEdgeFromRightUp()
            => new AlgorithmCollection(
                "Move edge from up right.",
                new PuzzleState("RightUp* Blue Red"),
                NotationMoveType.UpClockwise,
                NotationMoveType.MiddleMClockwise);

        private AlgorithmCollection MoveEdgeFromRightDown()
            => new AlgorithmCollection(
                "Move edge from right down.",
                new PuzzleState("RightDown* Blue Red"),
                NotationMoveType.RightDouble,
                NotationMoveType.UpClockwise,
                NotationMoveType.MiddleMClockwise);

        private AlgorithmCollection MoveEdgeFromLeftUp()
            => new AlgorithmCollection(
                "Move edge from right up.",
                new PuzzleState("LeftUp* Blue Red"),
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.MiddleMClockwise);

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.EdgeOrFlipped(puzzle, Location.FrontDown, this.Color1, this.Color2);
    }
}