namespace RubiksCubeTrainer.Puzzle3x3
{
    public class Puzzle
    {
        private readonly Face[] faces;

        public Puzzle(params Face[] faces)
        {
            this.faces = faces;
        }

        public Face this[FaceName faceName]
            => this.faces[(int)faceName];

        public PuzzleColor this[Location location]
        {
            get { return this[location.FaceName][CoordinateMapper.Map(location)]; }

            internal set { this[location.FaceName][CoordinateMapper.Map(location)] = value; }
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
    }
}