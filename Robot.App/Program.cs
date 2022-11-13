using Robot.Actors;
using Robot.Driver;
using Robot.Engine;
using Robot.Input;

var input = @"
                5 3
                1 1 E
                RFRFRFRF
                3 2 N
                FRRFLLFFRRFLL
                0 3 W
                LLFFFLFLFL
            ";

Console.WriteLine("Input");
Console.WriteLine(input);
Console.WriteLine("Start processing...");
Console.WriteLine("Press any key for exit");

var content = ContentReader.Read(input);

var batches = content.Batch.ToDictionary(x => new AgentId(Guid.NewGuid()), x => new AgentBatch(x.Item1, x.Item2));
var grid = new Grid(content.GridUpperRightPosition);

var commandHandler = new CommandHandler();
var driver = new RobotDriver(grid, commandHandler);

var appContext = new Robot.App.ActorsContext(driver);
var controllerId = new ControllerId(Guid.NewGuid());

var controllerActor = appContext.ControllerActorFactory(controllerId);
controllerActor.Start(batches);

Console.ReadKey();

