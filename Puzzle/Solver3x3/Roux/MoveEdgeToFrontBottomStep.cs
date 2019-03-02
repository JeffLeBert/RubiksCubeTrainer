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
               where algorithmInfo.InitialState(puzzle)
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
                PuzzleStateParser.Parse("FrontUp* Red Blue"),
                AlgorithmCollectionParser.ParseAlgorithms("F2"));

        private AlgorithmCollection MoveEdgeFromFrontLeft()
            => new AlgorithmCollection(
                "Move edge from front left.",
                PuzzleStateParser.Parse("FrontLeft* Red Blue"),
                AlgorithmCollectionParser.ParseAlgorithms("F'"));

        private AlgorithmCollection MoveEdgeFromFrontRight()
            => new AlgorithmCollection(
                "Move edge from front right.",
                PuzzleStateParser.Parse("FrontRight* Red Blue"),
                AlgorithmCollectionParser.ParseAlgorithms("F"));

        private AlgorithmCollection MoveEdgeFromBackLeft()
            => new AlgorithmCollection(
                "Move edge from back left.",
                PuzzleStateParser.Parse("LeftBack* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("B M'"));

        private AlgorithmCollection MoveEdgeFromBackUp()
            => new AlgorithmCollection(
                "Move edge from back up.",
                PuzzleStateParser.Parse("UpBack* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("M2"));

        private AlgorithmCollection MoveEdgeFromBackRight()
            => new AlgorithmCollection(
                "Move edge from back right.",
                PuzzleStateParser.Parse("BackRight* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("B U2 B' M"));

        private AlgorithmCollection MoveEdgeFromBackDown()
            => new AlgorithmCollection(
                "Move edge from back down.",
                PuzzleStateParser.Parse("BackDown* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("M'"));

        private AlgorithmCollection MoveEdgeFromRightUp()
            => new AlgorithmCollection(
                "Move edge from up right.",
                PuzzleStateParser.Parse("RightUp* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("U M"));

        private AlgorithmCollection MoveEdgeFromRightDown()
            => new AlgorithmCollection(
                "Move edge from right down.",
                PuzzleStateParser.Parse("RightDown* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("R2 U M"));

        private AlgorithmCollection MoveEdgeFromLeftUp()
            => new AlgorithmCollection(
                "Move edge from right up.",
                PuzzleStateParser.Parse("LeftUp* Blue Red"),
                AlgorithmCollectionParser.ParseAlgorithms("U' M"));

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.EdgeOrFlipped(puzzle, Location.FrontDown, this.Color1, this.Color2);
    }
}