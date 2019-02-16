using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{Name} - {Type}")]
    public struct NotationMoveType : IEquatable<NotationMoveType>
    {
        public static NotationMoveType None => new NotationMoveType();

        public NotationMoveType(NotationRotationNames name)
            : this(name, NotationRotationType.Clockwise)
        {
        }

        public NotationMoveType(NotationRotationNames name, NotationRotationType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public static NotationMoveType AllFrontLeft { get; } = new NotationMoveType(NotationRotationNames.AllFrontLeft, NotationRotationType.Clockwise);
        public static NotationMoveType AllFrontRight { get; } = new NotationMoveType(NotationRotationNames.AllFrontLeft, NotationRotationType.CounterClockwise);
        public static NotationMoveType AllFrontLeftDouble { get; } = new NotationMoveType(NotationRotationNames.AllFrontLeft, NotationRotationType.Double);

        public static NotationMoveType AllFrontClockwise { get; } = new NotationMoveType(NotationRotationNames.AllFrontClockwise, NotationRotationType.Clockwise);
        public static NotationMoveType AllFrontCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.AllFrontClockwise, NotationRotationType.CounterClockwise);
        public static NotationMoveType AllFrontClockwiseDouble { get; } = new NotationMoveType(NotationRotationNames.AllFrontClockwise, NotationRotationType.Double);

        public static NotationMoveType BackClockwise { get; } = new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Clockwise);
        public static NotationMoveType BackCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Back, NotationRotationType.CounterClockwise);
        public static NotationMoveType BackDouble { get; } = new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Double);

        public static NotationMoveType DownClockwise { get; } = new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise);
        public static NotationMoveType DownCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise);
        public static NotationMoveType DownDouble { get; } = new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Double);

        public static NotationMoveType FrontClockwise { get; } = new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Clockwise);
        public static NotationMoveType FrontCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Front, NotationRotationType.CounterClockwise);
        public static NotationMoveType FrontDouble { get; } = new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Double);

        public static NotationMoveType LeftClockwise { get; } = new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise);
        public static NotationMoveType LeftCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise);
        public static NotationMoveType LeftDouble { get; } = new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Double);

        public static NotationMoveType RightClockwise { get; } = new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Clockwise);
        public static NotationMoveType RightCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Right, NotationRotationType.CounterClockwise);
        public static NotationMoveType RightDouble { get; } = new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Double);

        public static NotationMoveType UpClockwise { get; } = new NotationMoveType(NotationRotationNames.Up, NotationRotationType.Clockwise);
        public static NotationMoveType UpCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.Up, NotationRotationType.CounterClockwise);
        public static NotationMoveType UpDouble { get; } = new NotationMoveType(NotationRotationNames.Up, NotationRotationType.Double);

        public static NotationMoveType MiddleMClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleM, NotationRotationType.Clockwise);
        public static NotationMoveType MiddleMCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleM, NotationRotationType.CounterClockwise);
        public static NotationMoveType MiddleMDouble { get; } = new NotationMoveType(NotationRotationNames.MiddleM, NotationRotationType.Double);

        public static NotationMoveType MiddleEClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleE, NotationRotationType.Clockwise);
        public static NotationMoveType MiddleECounterClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleE, NotationRotationType.CounterClockwise);
        public static NotationMoveType MiddleEDouble { get; } = new NotationMoveType(NotationRotationNames.MiddleE, NotationRotationType.Double);

        public static NotationMoveType MiddleSClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleS, NotationRotationType.Clockwise);
        public static NotationMoveType MiddleSCounterClockwise { get; } = new NotationMoveType(NotationRotationNames.MiddleS, NotationRotationType.CounterClockwise);
        public static NotationMoveType MiddleSDouble { get; } = new NotationMoveType(NotationRotationNames.MiddleS, NotationRotationType.Double);

        public bool IsEmpty => this.Name == NotationRotationNames.None;

        public NotationRotationNames Name { get; }

        public NotationRotationType Type { get; }

        public NotationMoveType With(NotationRotationNames rotationName)
            => new NotationMoveType(rotationName, this.Type);

        public NotationMoveType With(NotationRotationType rotationType)
            => new NotationMoveType(this.Name, rotationType);

        public NotationMoveType WithSwapDirection()
        {
            switch (this.Type)
            {
                case NotationRotationType.Clockwise:
                    return this.With(NotationRotationType.CounterClockwise);

                case NotationRotationType.CounterClockwise:
                    return this.With(NotationRotationType.Clockwise);

                case NotationRotationType.Double:
                    return this;

                default:
                    throw new InvalidOperationException();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is NotationMoveType type && this.Equals(type);
        }

        public bool Equals(NotationMoveType other)
            => this.Name == other.Name && this.Type == other.Type;

        public override int GetHashCode()
            => -243844509
            * (-1521134295 + this.Name.GetHashCode())
            * (-1521134295 + this.Type.GetHashCode());

        public static bool operator ==(NotationMoveType left, NotationMoveType right)
            => left.Equals(right);

        public static bool operator !=(NotationMoveType left, NotationMoveType right)
            => !(left == right);
    }
}