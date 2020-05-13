using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Okuma_Monitor_Tools.Events
{
    public class TimerErrorEvent
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public TimerErrorEvent(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
    }
}

