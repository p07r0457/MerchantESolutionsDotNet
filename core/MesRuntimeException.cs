using System;

namespace Mes.Core
{
    class MesRuntimeException : System.Exception
    {
        public MesRuntimeException() : base() { }
        public MesRuntimeException(string message) : base(message) { }
        public MesRuntimeException(string message, System.Exception inner) : base(message, inner) { }
    }
}
