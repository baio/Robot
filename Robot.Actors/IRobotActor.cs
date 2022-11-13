using Robot.Core;
using Robot.Driver;

namespace Robot.Actors
{
    public readonly record struct AgentId(Guid Id);

    public readonly record struct AgentBatch(State InitialState, IEnumerable<ICommand> Commands);

    public interface IAgentActor
    {
        void Execute(ControllerId controllerId, AgentBatch agentBatch, ISet<State> scents);
    }
}
