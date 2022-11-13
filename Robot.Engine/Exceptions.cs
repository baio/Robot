using Robot.Core;

namespace Robot.Engine
{
    public class InvalidEnumTypeArgumentException : ArgumentException
    {
        public InvalidEnumTypeArgumentException(string name, object obj) : base($"Invalid enum value for {name}", nameof(obj)) { }
    }

    public class DirectionEnumTypeArgumentException : InvalidEnumTypeArgumentException
    {
        public DirectionEnumTypeArgumentException(Direction obj) : base("direction", obj) { }
    }

    public class CommandTypeEnumTypeArgumentException : InvalidEnumTypeArgumentException
    {
        public CommandTypeEnumTypeArgumentException(CommandType obj) : base("commandType", obj) { }
    }

    public class IncompatibleCommandArgumentException : ArgumentException
    {
        public IncompatibleCommandArgumentException() : base("Command is incompatible with the CommandHandler") { }
    }
}
