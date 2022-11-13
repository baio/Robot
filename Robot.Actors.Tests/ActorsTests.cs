using Robot.Core;
using Robot.Driver;
using Robot.Engine;

namespace Robot.Actors.Tests
{

    public class Tests
    {
        static readonly List<AgentBatch> Batch1 = new()
        {
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
            ))
        };

        static readonly List<Result> Batch1Result =
            new() { new(new(new(1, 1), Direction.East), false), new(new(new(3, 3), Direction.North), true), new(new(new(2, 3), Direction.South), false) };

        static readonly List<(List<AgentBatch>, List<Result>)> ContextsProvider =
            new() { (Batch1, Batch1Result) };



        [Test, TestCaseSource(nameof(ContextsProvider))]
        public async Task TestActors((List<AgentBatch>, List<Result>) context)
        {
            // Arrange
            var agentBatches = context.Item1;
            var agentBatchesMap = agentBatches.ToDictionary(x => new AgentId(Guid.NewGuid()), x => x);
            var expected = context.Item2;

            var grid = new Grid(new(5, 3));
            var commandHandler = new CommandHandler();
            var driver = new RobotDriver(grid, commandHandler);
            var testContext = new TestContext(driver);
            var controllerId = new ControllerId(Guid.NewGuid());
            var controllerActor = testContext.ControllerActorFactory(controllerId);
            var printerActor = testContext.PrinterActorFactory(controllerId) as PrinterActor;

            // Act
            await controllerActor.Start(agentBatchesMap);
            await Task.Delay(500);
            var actual = printerActor.results.Select(x => x.Item2);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }
    }
}