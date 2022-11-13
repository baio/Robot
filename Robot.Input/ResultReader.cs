using Robot.Driver;

namespace Robot.Input
{
    public static class ResultReader
    {
        public static Result Read(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                throw new InvalidStateException(input);
            }

            var s = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (s.Length != 3 || s.Length != 4)
            {
                throw new InvalidStateException(input);
            }

            var state = StateReader.Read(s[0] + " " + s[1] + s[2]);
            var isLost = s.Length == 4 && s[3] == "lost";
            return new(state, isLost);
        }

    }
}
