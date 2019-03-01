using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    partial struct Location
    {
		public static Location Back { get; } = new Location(FaceName.Back, 0, 1, 0);
		public static Location BackLeft { get; } = new Location(FaceName.Back, -1, 1, 0);
		public static Location BackLeftUp { get; } = new Location(FaceName.Back, -1, 1, 1);
		public static Location BackLeftDown { get; } = new Location(FaceName.Back, -1, 1, -1);
		public static Location BackDown { get; } = new Location(FaceName.Back, 0, 1, -1);
		public static Location BackUp { get; } = new Location(FaceName.Back, 0, 1, 1);
		public static Location BackRight { get; } = new Location(FaceName.Back, 1, 1, 0);
		public static Location BackRightUp { get; } = new Location(FaceName.Back, 1, 1, 1);
		public static Location BackRightDown { get; } = new Location(FaceName.Back, 1, 1, -1);
		public static Location Down { get; } = new Location(FaceName.Down, 0, 0, -1);
		public static Location DownFront { get; } = new Location(FaceName.Down, 0, -1, -1);
		public static Location DownFrontLeft { get; } = new Location(FaceName.Down, -1, -1, -1);
		public static Location DownFrontRight { get; } = new Location(FaceName.Down, 1, -1, -1);
		public static Location DownLeft { get; } = new Location(FaceName.Down, -1, 0, -1);
		public static Location DownRight { get; } = new Location(FaceName.Down, 1, 0, -1);
		public static Location DownBack { get; } = new Location(FaceName.Down, 0, 1, -1);
		public static Location DownBackLeft { get; } = new Location(FaceName.Down, -1, 1, -1);
		public static Location DownBackRight { get; } = new Location(FaceName.Down, 1, 1, -1);
		public static Location Front { get; } = new Location(FaceName.Front, 0, -1, 0);
		public static Location FrontLeft { get; } = new Location(FaceName.Front, -1, -1, 0);
		public static Location FrontLeftUp { get; } = new Location(FaceName.Front, -1, -1, 1);
		public static Location FrontLeftDown { get; } = new Location(FaceName.Front, -1, -1, -1);
		public static Location FrontRight { get; } = new Location(FaceName.Front, 1, -1, 0);
		public static Location FrontRightUp { get; } = new Location(FaceName.Front, 1, -1, 1);
		public static Location FrontRightDown { get; } = new Location(FaceName.Front, 1, -1, -1);
		public static Location FrontUp { get; } = new Location(FaceName.Front, 0, -1, 1);
		public static Location FrontDown { get; } = new Location(FaceName.Front, 0, -1, -1);
		public static Location Left { get; } = new Location(FaceName.Left, -1, 0, 0);
		public static Location LeftBack { get; } = new Location(FaceName.Left, -1, 1, 0);
		public static Location LeftBackUp { get; } = new Location(FaceName.Left, -1, 1, 1);
		public static Location LeftBackDown { get; } = new Location(FaceName.Left, -1, 1, -1);
		public static Location LeftFront { get; } = new Location(FaceName.Left, -1, -1, 0);
		public static Location LeftFrontUp { get; } = new Location(FaceName.Left, -1, -1, 1);
		public static Location LeftFrontDown { get; } = new Location(FaceName.Left, -1, -1, -1);
		public static Location LeftUp { get; } = new Location(FaceName.Left, -1, 0, 1);
		public static Location LeftDown { get; } = new Location(FaceName.Left, -1, 0, -1);
		public static Location Right { get; } = new Location(FaceName.Right, 1, 0, 0);
		public static Location RightBack { get; } = new Location(FaceName.Right, 1, 1, 0);
		public static Location RightBackUp { get; } = new Location(FaceName.Right, 1, 1, 1);
		public static Location RightBackDown { get; } = new Location(FaceName.Right, 1, 1, -1);
		public static Location RightFront { get; } = new Location(FaceName.Right, 1, -1, 0);
		public static Location RightFrontUp { get; } = new Location(FaceName.Right, 1, -1, 1);
		public static Location RightFrontDown { get; } = new Location(FaceName.Right, 1, -1, -1);
		public static Location RightUp { get; } = new Location(FaceName.Right, 1, 0, 1);
		public static Location RightDown { get; } = new Location(FaceName.Right, 1, 0, -1);
		public static Location Up { get; } = new Location(FaceName.Up, 0, 0, 1);
		public static Location UpBack { get; } = new Location(FaceName.Up, 0, 1, 1);
		public static Location UpBackLeft { get; } = new Location(FaceName.Up, -1, 1, 1);
		public static Location UpBackRight { get; } = new Location(FaceName.Up, 1, 1, 1);
		public static Location UpFront { get; } = new Location(FaceName.Up, 0, -1, 1);
		public static Location UpFrontLeft { get; } = new Location(FaceName.Up, -1, -1, 1);
		public static Location UpFrontRight { get; } = new Location(FaceName.Up, 1, -1, 1);
		public static Location UpLeft { get; } = new Location(FaceName.Up, -1, 0, 1);
		public static Location UpRight { get; } = new Location(FaceName.Up, 1, 0, 1);
	}

	public static class LocationParser
	{
		public static Location Parse(string location)
		{
			switch (location)
			{
				case "Back": return Location.Back;
				case "BackLeft": return Location.BackLeft;
				case "BackLeftUp": return Location.BackLeftUp;
				case "BackLeftDown": return Location.BackLeftDown;
				case "BackDown": return Location.BackDown;
				case "BackUp": return Location.BackUp;
				case "BackRight": return Location.BackRight;
				case "BackRightUp": return Location.BackRightUp;
				case "BackRightDown": return Location.BackRightDown;
				case "Down": return Location.Down;
				case "DownFront": return Location.DownFront;
				case "DownFrontLeft": return Location.DownFrontLeft;
				case "DownFrontRight": return Location.DownFrontRight;
				case "DownLeft": return Location.DownLeft;
				case "DownRight": return Location.DownRight;
				case "DownBack": return Location.DownBack;
				case "DownBackLeft": return Location.DownBackLeft;
				case "DownBackRight": return Location.DownBackRight;
				case "Front": return Location.Front;
				case "FrontLeft": return Location.FrontLeft;
				case "FrontLeftUp": return Location.FrontLeftUp;
				case "FrontLeftDown": return Location.FrontLeftDown;
				case "FrontRight": return Location.FrontRight;
				case "FrontRightUp": return Location.FrontRightUp;
				case "FrontRightDown": return Location.FrontRightDown;
				case "FrontUp": return Location.FrontUp;
				case "FrontDown": return Location.FrontDown;
				case "Left": return Location.Left;
				case "LeftBack": return Location.LeftBack;
				case "LeftBackUp": return Location.LeftBackUp;
				case "LeftBackDown": return Location.LeftBackDown;
				case "LeftFront": return Location.LeftFront;
				case "LeftFrontUp": return Location.LeftFrontUp;
				case "LeftFrontDown": return Location.LeftFrontDown;
				case "LeftUp": return Location.LeftUp;
				case "LeftDown": return Location.LeftDown;
				case "Right": return Location.Right;
				case "RightBack": return Location.RightBack;
				case "RightBackUp": return Location.RightBackUp;
				case "RightBackDown": return Location.RightBackDown;
				case "RightFront": return Location.RightFront;
				case "RightFrontUp": return Location.RightFrontUp;
				case "RightFrontDown": return Location.RightFrontDown;
				case "RightUp": return Location.RightUp;
				case "RightDown": return Location.RightDown;
				case "Up": return Location.Up;
				case "UpBack": return Location.UpBack;
				case "UpBackLeft": return Location.UpBackLeft;
				case "UpBackRight": return Location.UpBackRight;
				case "UpFront": return Location.UpFront;
				case "UpFrontLeft": return Location.UpFrontLeft;
				case "UpFrontRight": return Location.UpFrontRight;
				case "UpLeft": return Location.UpLeft;
				case "UpRight": return Location.UpRight;
				default:
					throw new InvalidOperationException();
			}
		}
	}
}