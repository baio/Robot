using Robot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Driver
{
    public readonly record struct Result(State State, bool IsLost);

    public interface IRobotDriver
    {
        public Result ApplyCommands(State initialState, IEnumerable<ICommand> commands, ISet<State> lostScents);
    }
}
