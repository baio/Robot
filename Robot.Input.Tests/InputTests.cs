using Robot.Core;
using Robot.Engine;
using System.Text.RegularExpressions;

namespace Robot.Input.Tests
{
    public class Tests
    {

        [Test]
        public void InputTest()
        {
            var content = @"
                5 3
                1 1 E
                RFRFRFRF
                3 2 N
                FRRFLLFFRRFLL
                0 3 W
                LLFFFLFLFL
            ";

            var actual = ContentReader.Read(content);


            Content expected = new(new(5, 3), (new List<(State, IEnumerable<ICommand>)>() {
            (
                new(new(new(1, 1), Direction.East),
                    new List<Command>()
                    {
                        // RF RF RF RF
                        new(CommandType.Right),
                        new(CommandType.Forward),
                        new(CommandType.Right),
                        new(CommandType.Forward),
                        new(CommandType.Right),
                        new(CommandType.Forward),
                        new(CommandType.Right),
                        new(CommandType.Forward)
                    }.Select(x => x as ICommand))),
                (
                    new (new(new (3, 2), Direction.North),
                    new List<Command>() {
                        // FRRFLLFFRRFLL
                        new(CommandType.Forward),
                        new(CommandType.Right),
                        new(CommandType.Right),
                        new(CommandType.Forward),
                        new(CommandType.Left),
                        new(CommandType.Left),
                        new(CommandType.Forward),
                        new(CommandType.Forward),
                        new(CommandType.Right),
                        new(CommandType.Right),
                        new(CommandType.Forward),
                        new(CommandType.Left),
                        new(CommandType.Left)}.Select(x => x as ICommand)
                )),
                (
                    new (new(new (0, 3), Direction.West),
                        new List<Command>() {
                            // LLFFFLFLFL
                            new(CommandType.Left),//1
                            new(CommandType.Left),//2
                            new(CommandType.Forward),//3
                            new(CommandType.Forward),//4
                            new(CommandType.Forward),//5
                            new(CommandType.Left),//6
                            new(CommandType.Forward),//7
                            new(CommandType.Left),//8
                            new(CommandType.Forward),//9
                            new(CommandType.Left)}.Select(x => x as ICommand)
                )) }));
            Assert.Multiple(() =>
            {
                Assert.That(actual.GridUpperRightPosition, Is.EqualTo(expected.GridUpperRightPosition));
                Assert.That(actual.Batch, Is.EqualTo(expected.Batch));
            });
        }
    }
}