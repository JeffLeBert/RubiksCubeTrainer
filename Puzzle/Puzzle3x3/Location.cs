namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{FaceName} ({Point3D.X}, {Point3D.Y}, {Point3D.Z})")]
    public struct Location
    {
        public Location(FaceName faceName, int x, int y, int z)
            : this(faceName, new Point3D(x, y, z))
        {
        }

        public Location(FaceName faceName, Point3D point)
        {
            this.FaceName = faceName;
            this.Point3D = point;
        }

        public static Location BackCenter { get; } = new Location(FaceName.Back, 0, 1, 0);
        public static Location BackLeftEdge { get; } = new Location(FaceName.Back, -1, 1, 0);
        public static Location BackDownEdge { get; } = new Location(FaceName.Back, 0, 1, -1);
        public static Location BackUpEdge { get; } = new Location(FaceName.Back, 0, 1, 1);
        public static Location BackRightEdge { get; } = new Location(FaceName.Back, 1, 1, 0);

        public static Location DownCenter { get; } = new Location(FaceName.Down, 0, 0, -1);
        public static Location DownFrontEdge { get; } = new Location(FaceName.Down, 0, -1, -1);
        public static Location DownLeftEdge { get; } = new Location(FaceName.Down, -1, 0, -1);
        public static Location DownRightEdge { get; } = new Location(FaceName.Down, 1, 0, -1);
        public static Location DownBackEdge { get; } = new Location(FaceName.Down, 0, 1, -1);

        public static Location FrontCenter { get; } = new Location(FaceName.Front, 0, -1, 0);
        public static Location FrontLeftEdge { get; } = new Location(FaceName.Front, -1, -1, 0);
        public static Location FrontRightEdge { get; } = new Location(FaceName.Front, 1, -1, 0);
        public static Location FrontUpEdge { get; } = new Location(FaceName.Front, 0, -1, 1);
        public static Location FrontDownEdge { get; } = new Location(FaceName.Front, 0, -1, -1);

        public static Location LeftCenter { get; } = new Location(FaceName.Left, -1, 0, 0);
        public static Location LeftBackEdge { get; } = new Location(FaceName.Left, -1, 1, 0);
        public static Location LeftFrontEdge { get; } = new Location(FaceName.Left, -1, -1, 0);
        public static Location LeftUpEdge { get; } = new Location(FaceName.Left, -1, 0, 1);
        public static Location LeftDownEdge { get; } = new Location(FaceName.Left, -1, 0, -1);

        public static Location RightCenter { get; } = new Location(FaceName.Right, 1, 0, 0);
        public static Location RightBackEdge { get; } = new Location(FaceName.Right, 1, 1, 0);
        public static Location RightFrontEdge { get; } = new Location(FaceName.Right, 1, -1, 0);
        public static Location RightUpEdge { get; } = new Location(FaceName.Right, 1, 0, 1);
        public static Location RightDownEdge { get; } = new Location(FaceName.Right, 1, 0, -1);

        public static Location UpCenter { get; } = new Location(FaceName.Up, 0, 0, 1);
        public static Location UpBackEdge { get; } = new Location(FaceName.Up, 0, 1, 1);
        public static Location UpFrontEdge { get; } = new Location(FaceName.Up, 0, -1, 1);
        public static Location UpLeftEdge { get; } = new Location(FaceName.Up, -1, 0, 1);
        public static Location UpRightEdge { get; } = new Location(FaceName.Up, 1, 0, 1);

        public FaceName FaceName { get; }

        public Point2D Point2D => CoordinateMapper.GetMapperForFace(this.FaceName).Map(this.Point3D);

        public Point3D Point3D { get; }
    }
}