using Robot.Core;
using Robot.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Actors.AgentActor
{
    using ControllerActorFactory = Func<ControllerId, IControllerActor>;

    public readonly record struct Environment(AgentId Id, IRobotDriver Driver, ControllerActorFactory ControllerActorFactory);

    public class AgentActor : IAgentActor
    {
        public AgentActor(Environment environment)
        {
            Environment = environment;
        }

        public Environment Environment { get; }

        public void Execute(ControllerId controllerId, AgentBatch agentBatch, ISet<State> scents)
        {

            var initialState = agentBatch.InitialState;
            var commands = agentBatch.Commands;

            var result = Environment.Driver.ApplyCommands(initialState, commands, scents);

            var controllerActor = Environment.ControllerActorFactory(controllerId);

            controllerActor.AgentReport(Environment.Id, result);
        }
    }
}
