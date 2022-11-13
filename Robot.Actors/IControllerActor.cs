using Robot.Core;
using Robot.Driver;

namespace Robot.Actors
{
    public readonly record struct RobotId(Guid Id);

    public readonly record struct AgentBatch(RobotId AgentId, State InitialState, IEnumerable<ICommand> Commands);

    public interface IControllerActor
    {
        Task Start(AgentBatchesMap agentBatches);

        Task AgentReport(RobotId agentId, Result result);
    }
}