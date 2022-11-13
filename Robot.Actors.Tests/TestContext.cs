using Robot.Driver;

namespace Robot.Actors.Tests
{
    internal class TestContext
    {
        internal TestContext(IRobotDriver robotDriver)
        {
            RobotDriver = robotDriver;
        }

        private readonly Dictionary<ControllerId, IControllerActor> _controllerActors = new();
        private readonly Dictionary<AgentId, IAgentActor> _agentActors = new();
        private readonly Dictionary<ControllerId, IPrinterActor> _printerActors = new();

        private IRobotDriver RobotDriver { get; }

        private IControllerActor CreateControllerActor(ControllerId id)
        {
            var env = new ControllerActor.Environment(id, new ActorStorage<ControllerActor.State>(), AgentActorFactory, PrinterActorFactory);
            return new ControllerActor.ControllerActor(env);
        }

        internal IControllerActor ControllerActorFactory(ControllerId id) => GetOrCreateActor(_controllerActors, id, CreateControllerActor);

        private IAgentActor CreateAgentActor(AgentId id)
        {
            var env = new AgentActor.Environment(id, RobotDriver, ControllerActorFactory);
            return new AgentActor.AgentActor(env);
        }

        private IAgentActor AgentActorFactory(AgentId id) => GetOrCreateActor(_agentActors, id, CreateAgentActor);


        private IPrinterActor CreatePrinterActor(ControllerId id)
        {
            return new PrinterActor(id);
        }

        internal IPrinterActor PrinterActorFactory(ControllerId id) => GetOrCreateActor(_printerActors, id, CreatePrinterActor);

        private static V GetOrCreateActor<K, V>(IDictionary<K, V> dict, K key, Func<K, V> fn)
        {
            if (dict.ContainsKey(key)) { return dict[key]; } else { var v = fn(key); dict.Add(key, v); return v; }
        }

    }
}
