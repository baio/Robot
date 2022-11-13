using Robot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Driver
{
    public readonly record struct Grid(Position UpperRightPosition)
    {
        public static readonly Position BottomLeftPosition = new(0, 0);

        public bool IsInBounds(Position position) =>
            position.X >= BottomLeftPosition.X
            && position.Y >= BottomLeftPosition.Y
            && position.X <= UpperRightPosition.X
            && position.Y <= UpperRightPosition.Y;  

        public override string ToString()
        {
            return $"[{Grid.BottomLeftPosition} {UpperRightPosition}]";
        }

    }
}
