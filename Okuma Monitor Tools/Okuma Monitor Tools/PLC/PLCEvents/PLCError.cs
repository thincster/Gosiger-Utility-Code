using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma_Monitor_Tools.PLC.PLCEvents
{
    public class PLCError
    {
        public string ErrorMessage { get; internal set; }
        public PLCError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}