using Robot.Actors;
using Robot.Driver;

namespace Robot.App
{
    internal class PrinterActor : IPrinterActor
    {
        internal PrinterActor(ControllerId controllerId)
        {
            ControllerId = controllerId;
        }

        internal ControllerId ControllerId { get; }

        public void PrintResult(AgentId agentId, Result result)
        {
            Console.WriteLine($"{result.State.Position.X} {result.State.Position.Y} {result.State.Direction} {(result.IsLost ? "LOST" : "")}");
        }
    }
}
