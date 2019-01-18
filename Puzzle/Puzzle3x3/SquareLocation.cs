namespace RubiksCubeTrainer.Puzzle3x3
{
    public struct SquareLocation
    {
        private readonly int value;

        public SquareLocation(FaceName face, int index)
        {
            this.value = (int)face * 100 + index;
        }

        public static SquareLocation Empty { get; } = new SquareLocation(0, int.MinValue);

        public FaceName Face => (FaceName)(this.value / 100);

        public int Index => this.value % 100;

        public bool IsEmpty => value < 0;

        public override int GetHashCode() => this.value;
    }
}