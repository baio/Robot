using Robot.Core;

namespace Robot.Input
{
    public static class PositionReader
    {
        static int ReadInt(string s, string input)
        {
            if (int.TryParse(s, out var value))
            {
                return value;
            }
            else
            {
                throw new InvalidStateException(input);
            }
        }

        public static Position Read(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                throw new InvalidStateException(input);
            }

            var s = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (s.Length != 2)
            {
                throw new InvalidStateException(input);
            }

            var x = ReadInt(s[0], input);
            var y = ReadInt(s[1], input);
            return new(x, y);
        }
    }
}