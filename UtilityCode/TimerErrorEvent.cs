using System;

namespace UtilityCode
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
