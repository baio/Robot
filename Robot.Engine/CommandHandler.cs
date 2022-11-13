using Robot.Core;

namespace Robot.Engine
{
    public class CommandHandler : ICommandHandler
    {

        private static Direction RotateLef(Direction direction) => direction switch
        {
            Direction.North => Direction.West,
            Direction.East => Direction.North,
            Direction.South => Direction.East,
            Direction.West => Direction.South,
            _ => throw new DirectionEnumTypeArgumentException(direction)
        };

        private static Direction RotateRight(Direction direction) => direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new DirectionEnumTypeArgumentException(direction)
        };

        private static Position MoveForward(State state) => state.Direction switch
        {
            Direction.North => state.Position with { Y = state.Position.Y + 1 },
            Direction.East => state.Position with { X = state.Position.X + 1 },
            Direction.South => state.Position with { Y = state.Position.Y - 1 },
            Direction.West => state.Position with { Y = state.Position.X - 1 },
            _ => throw new DirectionEnumTypeArgumentException(state.Direction)
        };


        private static State Handle(State state, Command command) => command.CommandType switch
        {
            CommandType.Left => state with { Direction = RotateLef(state.Direction) },
            CommandType.Right => state with { Direction = RotateRight(state.Direction) },
            CommandType.Forward => state with { Position = MoveForward(state) },
            _ => throw new CommandTypeEnumTypeArgumentException(command.CommandType)
        };

        public State Handle(State state, ICommand command)
        {
            if (command is Command command1)
            { return Handle(state, command1); }
            else
            { throw new IncompatibleCommandArgumentException(); }
        }
    }
}
