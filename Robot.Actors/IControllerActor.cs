using Robot.Core;
using Robot.Driver;

namespace Robot.Actors
{
    public readonly record struct ControllerId(Guid Id);

    public interface IControllerActor
    {
        Task Start(AgentBatchesMap agentBatches);

        Task AgentReport(AgentId agentId, Result result);
    }
}