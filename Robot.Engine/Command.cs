using Robot.Core;

namespace Robot.Engine
{
    public enum CommandType
    {
        Left, Right, Forward
    }

    public readonly record struct Command(CommandType CommandType) : ICommand
    {
        public bool IsMove => CommandType == CommandType.Forward;

    }
}