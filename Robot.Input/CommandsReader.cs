using Robot.Core;
using Robot.Engine;

namespace Robot.Input
{
    public static class CommandsReader
    {
        static ICommand ReadCommand(char s, string input) => s switch
        {
            'l' => new Command(CommandType.Left),
            'r' => new Command(CommandType.Right),
            'f' => new Command(CommandType.Forward),
            _ => throw new InvalidStateException(input),
        };

        public static IEnumerable<ICommand> Read(string input)
        {

            if (String.IsNullOrEmpty(input))
            {
                throw new InvalidStateException(input);
            }

            var s = input.ToLower();

            if (s.Length > 100)
            {
                throw new InvalidCommandException(input);
            }


            return input.Select(x => ReadCommand(x, input));
        }
    }
}