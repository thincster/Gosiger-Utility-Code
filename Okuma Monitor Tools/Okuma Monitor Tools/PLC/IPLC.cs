
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Okuma_Monitor_Tools.PLC
{
    public interface IPLC
    {
        bool Connect();
        Task<bool> ConnectAsync();
        void Disconnect();
        bool IsAvailable { get; }
        bool IsConnected { get; }
        ToPLC ToPLC { get; set; }
        FromPLC FromPLC { get; set; }

        Task<bool> WaitForSignal(Func<bool> target, bool targetState, int timeout);

        string IP { get; }
    }
}

