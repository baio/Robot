using Robot.Core;

namespace Robot.Input
{
    public static class StateReader
    {
        public static State Read(string input)
        {

            if (String.IsNullOrEmpty(input))
            {
                throw new InvalidStateException(input);
            }

            var s = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (s.Length != 3)
            {
                throw new InvalidStateException(input);
            }

            var position = PositionReader.Read(s[0] + " " + s[1]);
            var direction = DirectionReader.Read(s[2]);
            return new(position, direction);
        }
    }
}