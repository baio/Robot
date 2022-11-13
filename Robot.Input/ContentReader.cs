using Robot.Core;

namespace Robot.Input
{
    public readonly record struct Content(
        Position GridUpperRightPosition,
        IEnumerable<(State, IEnumerable<ICommand>)> Batch);

    public static class ContentReader
    {
        private static IEnumerable<(string, string)> Pairwise(IEnumerable<string> input)
        {
            for (var i = 0; i < input.Count() - 1; i += 2)
            {
                yield return (input.ElementAt(i), input.ElementAt(i + 1));
            }
        }

        public static Content Read(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new InvalidContentException(input);
            }

            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim().ToLower()).ToArray();

            if (lines.Length < 3)
            {
                throw new InvalidContentException(input);
            }

            var gridString = lines[0];
            var batchStrings = Pairwise(lines.Skip(1));

            var gridPosition = PositionReader.Read(gridString);

            var batches = batchStrings.Select(x => (StateReader.Read(x.Item1), CommandsReader.Read(x.Item2)));

            return new(gridPosition, batches);
        }
    }
}
