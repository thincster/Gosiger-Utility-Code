using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace Okuma_Monitor_Tools.Utilities
{
   
        public static class NetworkCheck
        {

        // Create a logger for this class.
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static async Task WaitForNetwork(TimeSpan timeout)
            {
              
                var sw = new Stopwatch();

                sw.Restart();

                var cnt = 0;

                Logger.Warn($"Starting network check network status is currently {IsNetworkAvailable()}");

                while (!IsNetworkAvailable())
                {
                    Logger.Warn($"rechecking network status attempt {cnt}. {sw.Elapsed.TotalSeconds}S of {timeout.TotalSeconds}S timeout has elapsed ");
                    await TaskEx.Delay(5000);
                    if (sw.Elapsed >= timeout)
                    {
                        break;
                    }
                    cnt += 1;
                }

            }


            /// <param name="minimumSpeed">The minimum speed required. Passing 0 will not filter connection using speed.</param>
            /// <returns>
            ///     <c>true</c> if a network connection is available; otherwise, <c>false</c>.
            ///     https://stackoverflow.com/a/8345173
            /// </returns>
            public static bool IsNetworkAvailable(long minimumSpeed = 0)
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                    return false;

                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {

                    Logger.Warn($"{ni.Name} op status is {ni.OperationalStatus}");

                    // discard because of standard reasons
                    if ((ni.OperationalStatus != OperationalStatus.Up) ||
                        (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                        (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                        continue;

                    // this allow to filter modems, serial, etc.
                    // I use 10000000 as a minimum speed for most cases
                    if (ni.Speed < minimumSpeed)
                        continue;

                    // discard virtual cards (virtual box, virtual pc, etc.)
                    if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                        continue;

                    // discard "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                    if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                        continue;

                    return true;
                }
                return false;
            }

            public static bool PingHost(string nameOrAddress)
            {
                bool pingable = false;
                Ping pinger = null;

                try
                {
                    pinger = new Ping();
                    PingReply reply = pinger.Send(nameOrAddress);
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    // Discard PingExceptions and return false;
                }
                finally
                {
                    if (pinger != null)
                    {
                        pinger.Dispose();
                    }
                }

                return pingable;
            }
        }
    

}
