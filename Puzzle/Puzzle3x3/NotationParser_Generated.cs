using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    partial class NotationParser
    {
        private static string FormatMoveName(NotationRotationNames rotationName)
        {
            switch (rotationName)
            {
                case NotationRotationNames.Right: return "R";
                case NotationRotationNames.Left: return "L";
                case NotationRotationNames.Up: return "U";
                case NotationRotationNames.Down: return "D";
                case NotationRotationNames.Front: return "F";
                case NotationRotationNames.Back: return "B";
                case NotationRotationNames.WideRight: return "r";
                case NotationRotationNames.WideLeft: return "l";
                case NotationRotationNames.WideUp: return "u";
                case NotationRotationNames.WideDown: return "d";
                case NotationRotationNames.WideFront: return "f";
                case NotationRotationNames.WideBack: return "b";
                case NotationRotationNames.MiddleE: return "E";
                case NotationRotationNames.MiddleM: return "M";
                case NotationRotationNames.MiddleS: return "S";
                case NotationRotationNames.AllX: return "x";
                case NotationRotationNames.AllY: return "y";
                case NotationRotationNames.AllZ: return "z";

                default: throw new InvalidOperationException();
            }
        }


        private static NotationRotationNames ParseMoveName(char moveChar)
        {
            switch (moveChar)
            {
                case 'R': return NotationRotationNames.Right;
                case 'L': return NotationRotationNames.Left;
                case 'U': return NotationRotationNames.Up;
                case 'D': return NotationRotationNames.Down;
                case 'F': return NotationRotationNames.Front;
                case 'B': return NotationRotationNames.Back;
                case 'r': return NotationRotationNames.WideRight;
                case 'l': return NotationRotationNames.WideLeft;
                case 'u': return NotationRotationNames.WideUp;
                case 'd': return NotationRotationNames.WideDown;
                case 'f': return NotationRotationNames.WideFront;
                case 'b': return NotationRotationNames.WideBack;
                case 'E': return NotationRotationNames.MiddleE;
                case 'M': return NotationRotationNames.MiddleM;
                case 'S': return NotationRotationNames.MiddleS;
                case 'x': return NotationRotationNames.AllX;
                case 'y': return NotationRotationNames.AllY;
                case 'z': return NotationRotationNames.AllZ;

                default: throw new InvalidOperationException();
            }
        }

	}
}