namespace RubiksCubeTrainer.Puzzle3x3
{
    partial struct NotationMoveType
    {
		public static NotationMoveType RightClockwise { get; } = new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Clockwise);
		public static NotationMoveType RightCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Right, NotationRotationType.CounterClockwise);
		public static NotationMoveType RightDouble { get; } = new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Double);
		public static NotationMoveType LeftClockwise { get; } = new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise);
		public static NotationMoveType LeftCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise);
		public static NotationMoveType LeftDouble { get; } = new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Double);
		public static NotationMoveType UpClockwise { get; } = new NotationMoveType(NotationRotationNames.Up, NotationRotationType.Clockwise);
		public static NotationMoveType UpCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Up, NotationRotationType.CounterClockwise);
		public static NotationMoveType UpDouble { get; } = new NotationMoveType(NotationRotationNames.Up, NotationRotationType.Double);
		public static NotationMoveType DownClockwise { get; } = new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise);
		public static NotationMoveType DownCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise);
		public static NotationMoveType DownDouble { get; } = new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Double);
		public static NotationMoveType FrontClockwise { get; } = new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Clockwise);
		public static NotationMoveType FrontCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Front, NotationRotationType.CounterClockwise);
		public static NotationMoveType FrontDouble { get; } = new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Double);
		public static NotationMoveType BackClockwise { get; } = new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Clockwise);
		public static NotationMoveType BackCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Back, NotationRotationType.CounterClockwise);
		public static NotationMoveType BackDouble { get; } = new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Double);
		public static NotationMoveType WideRightClockwise { get; } = new NotationMoveType(NotationRotationNames.WideRight, NotationRotationType.Clockwise);
		public static NotationMoveType WideRightCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.WideRight, NotationRotationType.CounterClockwise);
		public static NotationMoveType WideRightDouble { get; } = new NotationMoveType(NotationRotationNames.WideRight, NotationRotationType.Double);
		public static NotationMoveType WideLeftClockwise { get; } = new NotationMoveType(NotationRotationNames.WideLeft, NotationRotationType.Clockwise);
		public static NotationMoveType WideLeftCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.WideLeft, NotationRotationType.CounterClockwise);
		public static NotationMoveType WideLeftDouble { get; } = new NotationMoveType(NotationRotationNames.WideLeft, NotationRotationType.Double);
		public static NotationMoveType WideUpClockwise { get; } = new NotationMoveType(NotationRotationNames.WideUp, NotationRotationType.Clockwise);
		public static NotationMoveType WideUpCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.WideUp, NotationRotationType.CounterClockwise);
		public static NotationMoveType WideUpDouble { get; } = new NotationMoveType(NotationRotationNames.WideUp, NotationRotationType.Double);
		public static NotationMoveType WideDownClockwise { get; } = new NotationMoveType(NotationRotationNames.WideDown, NotationRotationType.Clockwise);
		public static NotationMoveType WideDownCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.WideDown, NotationRotationType.CounterClockwise);
		public static NotationMoveType WideDownDouble { get; } = new NotationMoveType(NotationRotationNames.WideDown, NotationRotationType.Double);
		public static NotationMoveType WideFrontClockwise { get; } = new NotationMoveType(NotationRotationNames.WideFront, NotationRotationType.Clockwise);
		public static NotationMoveType WideFrontCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.WideFront, NotationRotationType.CounterClockwise);
		public static NotationMoveType WideFrontDouble { get; } = new NotationMoveType(NotationRotationNames.WideFront, NotationRotationType.Double);
		public static NotationMoveType WideBackClockwise { get; } = new NotationMoveType(NotationRotationNames.WideBack, NotationRotationType.Clockwise);
		public static NotationMoveType WideBackCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.WideBack, NotationRotationType.CounterClockwise);
		public static NotationMoveType WideBackDouble { get; } = new NotationMoveType(NotationRotationNames.WideBack, NotationRotationType.Double);
		public static NotationMoveType MiddleEClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleE, NotationRotationType.Clockwise);
		public static NotationMoveType MiddleECounterClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleE, NotationRotationType.CounterClockwise);
		public static NotationMoveType MiddleEDouble { get; } = new NotationMoveType(NotationRotationNames.MiddleE, NotationRotationType.Double);
		public static NotationMoveType MiddleMClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleM, NotationRotationType.Clockwise);
		public static NotationMoveType MiddleMCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleM, NotationRotationType.CounterClockwise);
		public static NotationMoveType MiddleMDouble { get; } = new NotationMoveType(NotationRotationNames.MiddleM, NotationRotationType.Double);
		public static NotationMoveType MiddleSClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleS, NotationRotationType.Clockwise);
		public static NotationMoveType MiddleSCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleS, NotationRotationType.CounterClockwise);
		public static NotationMoveType MiddleSDouble { get; } = new NotationMoveType(NotationRotationNames.MiddleS, NotationRotationType.Double);
		public static NotationMoveType AllXClockwise { get; } = new NotationMoveType(NotationRotationNames.AllX, NotationRotationType.Clockwise);
		public static NotationMoveType AllXCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.AllX, NotationRotationType.CounterClockwise);
		public static NotationMoveType AllXDouble { get; } = new NotationMoveType(NotationRotationNames.AllX, NotationRotationType.Double);
		public static NotationMoveType AllYClockwise { get; } = new NotationMoveType(NotationRotationNames.AllY, NotationRotationType.Clockwise);
		public static NotationMoveType AllYCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.AllY, NotationRotationType.CounterClockwise);
		public static NotationMoveType AllYDouble { get; } = new NotationMoveType(NotationRotationNames.AllY, NotationRotationType.Double);
		public static NotationMoveType AllZClockwise { get; } = new NotationMoveType(NotationRotationNames.AllZ, NotationRotationType.Clockwise);
		public static NotationMoveType AllZCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.AllZ, NotationRotationType.CounterClockwise);
		public static NotationMoveType AllZDouble { get; } = new NotationMoveType(NotationRotationNames.AllZ, NotationRotationType.Double);
    }
}