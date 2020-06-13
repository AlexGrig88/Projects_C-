using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingCar.Game
{
    class EngineIsDeadException : Exception
    {
        public EngineIsDeadException() { }
        public EngineIsDeadException(string message) : base(message) { }
        public EngineIsDeadException(string message, Exception inner) : base(message, inner) { }
    }
}
