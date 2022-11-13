using Robot.Core;
using Robot.Engine;
using System.Diagnostics;
using System.Collections.Generic;


namespace Robot.Driver.Tests
{
    public class RobotDriverTests
    {
        static readonly List<(State, IEnumerable<Command>, HashSet<State>, Result)> ContextsProvider = new(){
           (
                // 1 1 E
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
                new HashSet<State>(),
                new Result(new (new(1,1), Direction.East), false))),
           (
                // 3 2 N
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
                new HashSet<State>  (),
                new Result(new (new(3,3), Direction.North), true))),
           (
                // 0 3 W
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
                    new(CommandType.Left)},//10
                new HashSet <State>() { new (new(3,3), Direction.North) },
                new Result(new (new(2,3), Direction.South), false)))};

        [SetUp]
        public void Setup()
        {
        }


        [Test, TestCaseSource(nameof(ContextsProvider))]
        public void SampleTests((State, IEnumerable<Command>, HashSet<State>, Result) context)
        {
            // Arrange
            var initialState = context.Item1;
            var commands = context.Item2.Select(x => x as ICommand);
            var lostScents = context.Item3;
            var expected = context.Item4;

            var grid = new Grid(new(5, 3));
            var commandHandler = new CommandHandler();
            var driver = new RobotDriver(grid, commandHandler);

            // Act
            var actual = driver.ApplyCommands(initialState, commands, lostScents);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}