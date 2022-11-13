using Robot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Input
{
    public readonly record struct Content(
        Position GridUpperRightPosition,
        IEnumerable<(Position, IEnumerable<ICommand>)> Batch);

    public static class ContentReader
    {
        public static Content Read(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new InvalidContentException(input);
            }

            var lines = input.Split('\n').Where(s => string.IsNullOrEmpty(s)).Select(s => s.Trim().ToLower()).ToArray();

            if (lines.Length < 3)
            {
                throw new InvalidContentException(input);
            }

            var gridString = lines[0];
            var batchStrings = lines.Skip(1).Zip(lines.Skip(2));

            var gridPosition = PositionReader.Read(gridString);

            var batches = batchStrings.Select(x => (PositionReader.Read(x.First), CommandsReader.Read(x.Second)));

            return new(gridPosition, batches);
        }
    }
}
