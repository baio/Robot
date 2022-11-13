using Robot.Core;
using Robot.Driver;

namespace Robot.Actors
{
    public interface IRobotActor
    {
        Task Execute(AgentBatch agentBatch, ISet<State> scents);
    }
}
