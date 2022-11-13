using Robot.Core;
using Robot.Engine;
using System.Diagnostics;
using System.Collections.Generic;


/*
 * Sample Input
5 3
1 1 E
RF RF RF RF
3 2 N
FRRFLLFFRRFLL
03 W
LLFFFLFLFL

Sample Output
11 E
3 3 N LOST
2 3 S
*/
namespace Robot.Driver.Tests
{


    public class RobotDriverTests
    {
        static readonly List<(State, IEnumerable<Command>, Result)> ItemsProvider = new(){
           (
                new (new(new (1, 1), Direction.East),
                new List<Command>() {
                    // RF RF RF RF
                    new(CommandType.Right),
                    new(CommandType.Forward),
                    new(CommandType.Right),
                    new(CommandType.Forward),
                    new(CommandType.Right),
                    new(CommandType.Forward),
                    new(CommandType.Right),
                    new(CommandType.Forward) },
                new Result(new (new(1,1), Direction.East), false))),
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
                    new(CommandType.Left)},
                new Result(new (new(3,3), Direction.North), true))),
                   (
                new (new(new (0, 3), Direction.West),
                new List<Command>() {
                    // LLFFFLFLFL
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
                    new(CommandType.Left)},//10
                new Result(new (new(2,3), Direction.South), false)))};

        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource(nameof(ItemsProvider))]
        public void SampleTests((State, IEnumerable<Command>, Result) item)
        {
            // Arrange
            var initialState = item.Item1;
            var commands = item.Item2.Select(x => x as ICommand);
            var expected = item.Item3;

            var grid = new Grid(new(5, 3));
            var commandHandler = new CommandHandler();
            var driver = new RobotDriver(grid, commandHandler);

            // Act
            var actual = driver.ApplyCommands(initialState, commands);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}