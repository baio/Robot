using Robot.Core;
using Robot.Driver;

namespace Robot.Actors.ControllerActor
{
    using AgentActorFactory = Func<AgentId, IAgentActor>;

    public readonly record struct State(AgentId? ActiveAgentId, AgentBatchesMap Stack, ISet<Core.State> Scents);

    public readonly record struct Environment(ControllerId Id, IActorStorage<State> State, AgentActorFactory RobotFactory);

    public class ControllerActor : IControllerActor
    {
        public ControllerActor(Environment environment)
        {
            Environment = environment;
        }

        public Environment Environment { get; }

        public async Task AgentReport(AgentId agentId, Result result)
        {
            var stateEntity = await Environment.State.Get();

            if (stateEntity.HasValue)
            {
                var state = stateEntity.Value;
                if (agentId != state.ActiveAgentId)
                {
                    throw new ActiveAgentMissmatchException(state.ActiveAgentId, agentId);
                }

                if (result.IsLost)
                {
                    state.Scents.Add(result.State);
                    var newState = state with { Scents = state.Scents };
                    await Environment.State.Set(newState);
                }

                await ExecNextBatch();
            }
            else
            {
                throw new StateNotFoundException();
            }
        }

        public async Task Start(AgentBatchesMap agentBatches)
        {
            var stateEntity = await Environment.State.Get();

            if (!stateEntity.HasValue)
            {
                var newState = new State(null, agentBatches, new HashSet<Core.State>());
                await Environment.State.Set(newState);
                await ExecNextBatch();
            }
            else
            {
                throw new ActorAlreadyStartedException();
            }
        }

        public async Task<bool> ExecNextBatch()
        {
            var stateEntity = await Environment.State.Get();

            if (!stateEntity.HasValue)
            {
                throw new StateNotFoundException();
            }

            var state = stateEntity.Value;

            if (state.Stack.Count != 0)
            {
                var nextRobot = state.Stack.First();

                var robotId = nextRobot.Key;
                var robotBatch = nextRobot.Value;

                var robotActor = Environment.RobotFactory(robotId);

                robotActor.Execute(Environment.Id, robotBatch, state.Scents);

                // Its ref but in actor env thats ok
                state.Stack.Remove(robotId);

                var newState = state with { ActiveAgentId = robotId, Stack = state.Stack };

                await Environment.State.Set(newState);

                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
