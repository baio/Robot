using Robot.Core;

namespace Robot.Driver
{
    public class InitailPositionOutOfGridArgumentException : ArgumentException
    {
        public InitailPositionOutOfGridArgumentException(Grid grid, Position position) : base($"Initial position {position} out of girid {grid}") { }
    }
}
