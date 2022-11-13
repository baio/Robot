namespace Robot.Core
{
    public interface ICommandHandler
    {
        State Handle(State state, ICommand command);
    }
}
