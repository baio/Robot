using Robot.Core;
using Robot.Driver;

namespace Robot.Actors
{

    public interface IActorStateEntity<T>
    {
        bool HasValue { get; }
        T Value { get; }
    }


    public interface IActorState<T>
    {
        Task<IActorStateEntity<T>> Get();
        Task Set(T Val);
    }

    public class ActorException : Exception
    {
        public ActorException(string Message) : base(Message) { }
    }

    public class ActorAlreadyStartedException : ActorException
    {
        public ActorAlreadyStartedException() : base("Actor already started") { }
    }

    public class StateNotFoundException : ActorException
    {
        public StateNotFoundException() : base("State not found") { }
    }

    public class ActiveAgentMissmatchException : ActorException
    {
        public ActiveAgentMissmatchException(RobotId? ExpectedRobotId, RobotId ActualRobotId) : base($"Active agent mismatch, expected : {ExpectedRobotId}, actual : {ActualRobotId} ") { }
    }

    public readonly record struct ControllerActorState(RobotId? ActiveAgentId, AgentBatchesMap Stack, ISet<State> Scents);

    public readonly record struct ControllerActorEnvironment(IActorState<ControllerActorState> State, Func<RobotId, IRobotActor> RobotFactory);

    public class ControllerActor : IControllerActor
    {
        public ControllerActor(ControllerActorEnvironment environment)
        {
            Environment = environment;
        }

        public ControllerActorEnvironment Environment { get; }

        public async Task AgentReport(RobotId agentId, Result result)
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

        public async Task Start(IDictionary<RobotId, AgentBatch> agentBatches)
        {
            var stateEntity = await Environment.State.Get();

            if (!stateEntity.HasValue)
            {
                var newState = new ControllerActorState(null, agentBatches, new HashSet<State>());
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

                await robotActor.Execute(robotBatch, state.Scents);

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
