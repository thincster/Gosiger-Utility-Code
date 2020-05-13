using System;
using System.Runtime.InteropServices;

namespace Okuma_Monitor_Tools.Utilities


{
    public class IPAdder

    {
        [
            DllImport("iphlpapi.dll", EntryPoint = "AddIPAddress", SetLastError = true)]
        private static extern uint MyAddIPAddress(uint Address, uint IpMaskint, int IfIndex,
            out IntPtr NTEContext, out IntPtr NTEInstance);

        public static void AddIPAddress(string IPAddress, string SubnetMask, int ifIndex)

        {
            var IPAdd = System.Net.IPAddress.Parse(IPAddress);

            var SubNet = System.Net.IPAddress.Parse(SubnetMask);

            var MyNTEContext = 0;

            var MyNTEInstance = 0;

            var ptrMyNTEContext = new IntPtr(MyNTEContext);

            var ptrMyNTEInstance = new IntPtr(MyNTEInstance);

            var Result = MyAddIPAddress((uint) IPAdd.Address,
                (
                    uint) SubNet.Address,
                ifIndex,
                out ptrMyNTEContext, out ptrMyNTEInstance);
            ;
        }
    }
}