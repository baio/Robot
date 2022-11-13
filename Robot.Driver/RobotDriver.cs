using Robot.Core;

namespace Robot.Driver
{
    public readonly record struct Result(State State, bool IsLost);

    public class RobotDriver
    {
        public RobotDriver(Grid grid, ICommandHandler commandHandler)
        {
            Grid = grid;
            CommandHandler = commandHandler;
        }

        public Grid Grid { get; }
        public ICommandHandler CommandHandler { get; }

        public Result ApplyCommands(State initialState, IEnumerable<ICommand> commands)
        {
            if (!Grid.IsInBounds(initialState.Position))
            {
                throw new InitailPositionOutOfGridArgumentException(Grid, initialState.Position);
            }

            // Can't use aggregate / reduce here since possiblity of yearly exit

            var state = initialState;

            foreach (var command in commands)
            {
                var newState = CommandHandler.Handle(state, command);

                if (Grid.IsInBounds(newState.Position))
                {
                    state = newState;
                }
                else
                {
                    return new Result(state, true);
                }
            }

            return new Result(state, false);
        }

    }
}