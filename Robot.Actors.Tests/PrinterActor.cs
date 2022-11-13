using Robot.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Actors.Tests
{
    internal class PrinterActor : IPrinterActor
    {
        internal PrinterActor(ControllerId controllerId)
        {
            ControllerId = controllerId;
        }

        internal readonly List<(AgentId, Result)> results = new();

        internal ControllerId ControllerId { get; }

        public void PrintResult(AgentId agentId, Result result)
        {
            results.Add((agentId, result));
        }
    }
}
