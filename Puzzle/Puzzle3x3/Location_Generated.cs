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
			if ("Back".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.Back;
			if ("BackLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackLeft;
			if ("BackLeftUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackLeftUp;
			if ("BackLeftDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackLeftDown;
			if ("BackDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackDown;
			if ("BackUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackUp;
			if ("BackRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackRight;
			if ("BackRightUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackRightUp;
			if ("BackRightDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.BackRightDown;
			if ("Down".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.Down;
			if ("DownFront".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownFront;
			if ("DownFrontLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownFrontLeft;
			if ("DownFrontRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownFrontRight;
			if ("DownLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownLeft;
			if ("DownRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownRight;
			if ("DownBack".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownBack;
			if ("DownBackLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownBackLeft;
			if ("DownBackRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.DownBackRight;
			if ("Front".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.Front;
			if ("FrontLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontLeft;
			if ("FrontLeftUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontLeftUp;
			if ("FrontLeftDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontLeftDown;
			if ("FrontRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontRight;
			if ("FrontRightUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontRightUp;
			if ("FrontRightDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontRightDown;
			if ("FrontUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontUp;
			if ("FrontDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.FrontDown;
			if ("Left".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.Left;
			if ("LeftBack".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftBack;
			if ("LeftBackUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftBackUp;
			if ("LeftBackDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftBackDown;
			if ("LeftFront".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftFront;
			if ("LeftFrontUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftFrontUp;
			if ("LeftFrontDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftFrontDown;
			if ("LeftUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftUp;
			if ("LeftDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.LeftDown;
			if ("Right".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.Right;
			if ("RightBack".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightBack;
			if ("RightBackUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightBackUp;
			if ("RightBackDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightBackDown;
			if ("RightFront".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightFront;
			if ("RightFrontUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightFrontUp;
			if ("RightFrontDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightFrontDown;
			if ("RightUp".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightUp;
			if ("RightDown".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.RightDown;
			if ("Up".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.Up;
			if ("UpBack".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpBack;
			if ("UpBackLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpBackLeft;
			if ("UpBackRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpBackRight;
			if ("UpFront".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpFront;
			if ("UpFrontLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpFrontLeft;
			if ("UpFrontRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpFrontRight;
			if ("UpLeft".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpLeft;
			if ("UpRight".Equals(location, StringComparison.OrdinalIgnoreCase)) return Location.UpRight;

			throw new InvalidOperationException();
		}

		public static string Format(Location location)
		{
			if (location == Location.Back) return "Back";
			if (location == Location.BackLeft) return "BackLeft";
			if (location == Location.BackLeftUp) return "BackLeftUp";
			if (location == Location.BackLeftDown) return "BackLeftDown";
			if (location == Location.BackDown) return "BackDown";
			if (location == Location.BackUp) return "BackUp";
			if (location == Location.BackRight) return "BackRight";
			if (location == Location.BackRightUp) return "BackRightUp";
			if (location == Location.BackRightDown) return "BackRightDown";
			if (location == Location.Down) return "Down";
			if (location == Location.DownFront) return "DownFront";
			if (location == Location.DownFrontLeft) return "DownFrontLeft";
			if (location == Location.DownFrontRight) return "DownFrontRight";
			if (location == Location.DownLeft) return "DownLeft";
			if (location == Location.DownRight) return "DownRight";
			if (location == Location.DownBack) return "DownBack";
			if (location == Location.DownBackLeft) return "DownBackLeft";
			if (location == Location.DownBackRight) return "DownBackRight";
			if (location == Location.Front) return "Front";
			if (location == Location.FrontLeft) return "FrontLeft";
			if (location == Location.FrontLeftUp) return "FrontLeftUp";
			if (location == Location.FrontLeftDown) return "FrontLeftDown";
			if (location == Location.FrontRight) return "FrontRight";
			if (location == Location.FrontRightUp) return "FrontRightUp";
			if (location == Location.FrontRightDown) return "FrontRightDown";
			if (location == Location.FrontUp) return "FrontUp";
			if (location == Location.FrontDown) return "FrontDown";
			if (location == Location.Left) return "Left";
			if (location == Location.LeftBack) return "LeftBack";
			if (location == Location.LeftBackUp) return "LeftBackUp";
			if (location == Location.LeftBackDown) return "LeftBackDown";
			if (location == Location.LeftFront) return "LeftFront";
			if (location == Location.LeftFrontUp) return "LeftFrontUp";
			if (location == Location.LeftFrontDown) return "LeftFrontDown";
			if (location == Location.LeftUp) return "LeftUp";
			if (location == Location.LeftDown) return "LeftDown";
			if (location == Location.Right) return "Right";
			if (location == Location.RightBack) return "RightBack";
			if (location == Location.RightBackUp) return "RightBackUp";
			if (location == Location.RightBackDown) return "RightBackDown";
			if (location == Location.RightFront) return "RightFront";
			if (location == Location.RightFrontUp) return "RightFrontUp";
			if (location == Location.RightFrontDown) return "RightFrontDown";
			if (location == Location.RightUp) return "RightUp";
			if (location == Location.RightDown) return "RightDown";
			if (location == Location.Up) return "Up";
			if (location == Location.UpBack) return "UpBack";
			if (location == Location.UpBackLeft) return "UpBackLeft";
			if (location == Location.UpBackRight) return "UpBackRight";
			if (location == Location.UpFront) return "UpFront";
			if (location == Location.UpFrontLeft) return "UpFrontLeft";
			if (location == Location.UpFrontRight) return "UpFrontRight";
			if (location == Location.UpLeft) return "UpLeft";
			if (location == Location.UpRight) return "UpRight";

			throw new InvalidOperationException();
		}
	}
}