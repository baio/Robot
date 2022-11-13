using Robot.Core;

namespace Robot.Input
{
    public static class DirectionReader
    {
        static Direction ReadDirection(string s, string input) => s switch
        {
            "n" => Direction.North,
            "s" => Direction.South,
            "e" => Direction.East,
            "w" => Direction.West,
            _ => throw new InvalidStateException(input),
        };


        public static Direction Read(string input)
        {

            if (String.IsNullOrEmpty(input))
            {
                throw new InvalidStateException(input);
            }

            var s = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (s.Length != 1)
            {
                throw new InvalidStateException(input);
            }

            var direction = ReadDirection(s[0], input);
            return direction;
        }
    }
}