using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveEdgeToFrontBottomStep : StepBase
    {
        public MoveEdgeToFrontBottomStep(PuzzleColor color1, PuzzleColor color2)
            : base()
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
            => from stepInfo in this.AllAlgorithms
               where stepInfo.PassesAllChecks(puzzle)
               select stepInfo;

        protected override AlgorithmInformation[] BuildAllAlgorithms()
            => new AlgorithmInformation[]
            {
                this.MoveEdgeFromFrontTop()
            };

        private AlgorithmInformation MoveEdgeFromFrontTop()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front top.",
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Double)),
                this.EndGoal.Checkers);

        protected override Goal BuildEndGoal()
        {
            var goal = new Goal(FirstPieceToBottomLeftEdgeStep.Instance.EndGoal);
            goal.Checkers.Add(
                new OrChecker(
                    new EdgeChecker(
                        new Location(FaceName.Front, 0, -1, -1),
                        this.Color1,
                        this.Color2),
                    new EdgeChecker(
                        new Location(FaceName.Front, 0, -1, -1),
                        this.Color2,
                        this.Color1)));

            return goal;
        }
    }
}