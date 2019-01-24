using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{Name} - {Type}")]
    public struct NotationMoveType
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

        public bool Empty => this.Name == NotationRotationNames.None;

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
    }
}