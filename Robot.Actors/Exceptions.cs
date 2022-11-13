namespace Robot.Actors
{
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
        public ActiveAgentMissmatchException(AgentId? ExpectedRobotId, AgentId ActualRobotId) : base($"Active agent mismatch, expected : {ExpectedRobotId}, actual : {ActualRobotId} ") { }
    }

}
