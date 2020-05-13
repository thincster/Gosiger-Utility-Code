using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace Okuma_Monitor_Tools.Utilities
{
    public class CheckIP
    {
        // Create a logger for this class.
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        // counter
        int _checkIPCount = 0;

        public CheckIP(string subnet, string ip)
        {
            
            string targetIP = $"{subnet}.{ip}";
            //string targetIP = string.Format("{0}.1{1}", subnet, Okuma.Scout.DMC.SerialNumber.Replace("P", "").Right(2).TrimStart('0'));
            _logger.Warn($"Target IP is {targetIP}");
            IPAddress ipa = IPAddress.Any;


            while (_checkIPCount < 4)
            {
                _checkIPCount += 1;
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if ((ni.OperationalStatus != OperationalStatus.Up) ||
                        (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                        (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                        continue;
                    if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                        continue;
                    if ((ni.Description.IndexOf("VM", StringComparison.OrdinalIgnoreCase) >= 0))
                        continue;
                    if ((ni.Description.IndexOf("Wireless", StringComparison.OrdinalIgnoreCase) >= 0))
                        continue;
                    if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                        continue;
                    IPInterfaceProperties ipip = ni.GetIPProperties();

                    _logger.Warn($"Getting ready to add IP Target nic is {ni.Name}");

                    bool foundTargetIp = false;
                    foreach (IPAddressInformation unic in ipip.UnicastAddresses)
                    {
                        string strip = unic.Address.ToString();
                        _logger.Warn($"{ni.Name} contains address {strip}");
                        if (strip == targetIP)
                        {
                            ipa = unic.Address;
                            foundTargetIp = true;
                            break;
                        }
                    }

                    var inx = (uint) ni.GetType()
                        .GetField("index", System.Reflection.BindingFlags.Public
                                           | System.Reflection.BindingFlags.NonPublic
                                           | System.Reflection.BindingFlags.Instance)
                        .GetValue(ni);


                    if (!foundTargetIp)
                    {
                        _logger.Warn(
                            $"IPAddress {targetIP} not found. Attempting to add it. Check count is {_checkIPCount}");
                        IPAdder.AddIPAddress(targetIP, "255.255.255.0", (int) inx);



                        if (_checkIPCount > 4)
                       
                        {
                            _logger.Fatal($"Failed to set IP to '{targetIP}' after {_checkIPCount} attempts.");
                           
                            throw new TimeoutException(
                                $"Failed to set IP to '{targetIP}' after {_checkIPCount} attempts.");
                           
                        }
                    }
                    else
                    {
                        _logger.Warn($"IPAddress {targetIP} has been added");
                        
                        break;
                    }
                }
            }
        }

    }
}
