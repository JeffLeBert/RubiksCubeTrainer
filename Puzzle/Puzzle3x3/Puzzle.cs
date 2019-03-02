using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class Puzzle
    {
        private readonly Face[] faces;

        public Puzzle(params Face[] faces)
        {
            this.faces = faces;
        }

        public static Puzzle Solved { get; } = BuildSolvedPuzzle();

        public Face this[FaceName faceName]
            => this.faces[(int)faceName];

        public PuzzleColor this[Location location]
        {
            get { return this[location.FaceName][MapTo2D(location)]; }

            internal set { this[location.FaceName][MapTo2D(location)] = value; }
        }

        public Puzzle Clone()
            => new Puzzle(
                new Face[]
                {
                    this.faces[0].Clone(),
                    this.faces[1].Clone(),
                    this.faces[2].Clone(),
                    this.faces[3].Clone(),
                    this.faces[4].Clone(),
                    this.faces[5].Clone()
                });

        public override string ToString()
            => new TextRenderer(this).Draw();

        private static Point2D MapTo2D(Location location)
        {
            var point3D = location.Point3D;
            switch (location.FaceName)
            {
                case FaceName.Up:
                case FaceName.Down:
                    return new Point2D(point3D.X, point3D.Y);

                case FaceName.Front:
                case FaceName.Back:
                    return new Point2D(point3D.X, point3D.Z);

                case FaceName.Left:
                case FaceName.Right:
                    return new Point2D(point3D.Y, point3D.Z);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static Puzzle BuildSolvedPuzzle()
            => new Puzzle(
                new Face(FaceName.Up, PuzzleColor.Yellow),
                new Face(FaceName.Front, PuzzleColor.Red),
                new Face(FaceName.Down, PuzzleColor.White),
                new Face(FaceName.Back, PuzzleColor.Orange),
                new Face(FaceName.Left, PuzzleColor.Blue),
                new Face(FaceName.Right, PuzzleColor.Green));
    }
}