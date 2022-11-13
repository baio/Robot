using Robot.Core;

namespace Robot.Driver
{
    public class RobotDriver : IRobotDriver
    {
        public RobotDriver(Grid grid, ICommandHandler commandHandler)
        {
            Grid = grid;
            CommandHandler = commandHandler;
        }

        public Grid Grid { get; }
        public ICommandHandler CommandHandler { get; }

        public Result ApplyCommands(State initialState, IEnumerable<ICommand> commands, ISet<State> lostScents)
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

                var isLostScent = command.IsMove && lostScents.Contains(state);

                if (!isLostScent)
                {
                    if (Grid.IsInBounds(newState.Position))
                    {
                        state = newState;
                    }
                    else
                    {
                        return new Result(state, true);
                    }
                }
            }

            return new Result(state, false);
        }

    }
}