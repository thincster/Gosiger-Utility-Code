using Caliburn.Micro;
using Okuma_Monitor_Tools.Events;
using Okuma_Monitor_Tools.PLC.PLCEvents;
using S7.Net;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UtilityCode;

namespace Okuma_Monitor_Tools.PLC
{

    public class S7Net : NeverEndingTask, IDisposable, IPLC
        {
            private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
            Plc _plc;
            string _ip;
            
            bool _scanning;
            
            
        //Stopwatch _sw1;
        //Stopwatch _sw2;

        public S7Net() : base("PLC")
        {
            string com1 = My.Settings.OkumaCom;
            string com2 = My.Settings.PlcCom;
            string targetIp = $"{My.Settings.IPSubnet}";
           int plcIP = 0;
           

            try
                {
                    if (string.IsNullOrWhiteSpace(com2) || com2.Length < 1 || com2.Length > 3)
                    {
                       _logger.Fatal($"PlcCom {com2} has not been setup yet");
                        throw new Exception($"PlcCom {com2} has not been setup yet");
                    }

                    if (com1 == com2)
                    {
                       _logger.Fatal($"OkumaCom {com1} cannot match PLCCom2 {com2}");
                        throw  new Exception($"OkumaCom {com1} cannot match PLCCom2 {com2}");
                    }

                 bool   canconvert = int.TryParse(com2, out plcIP);
                    if (plcIP < 1 && plcIP > 254)
                    {
                        _logger.Fatal($"PlcCom {com2} is out of range. Must be between 1 and 254");
                        throw  new Exception($"PlcCom {com2} is out of range. Must be between 1 and 254");
                    }

                    
                    _ip = $"{My.Settings.IPSubnet}.{My.Settings.PlcCom}";

                    _plc = new Plc(CpuType.S71200, _ip, 0, 1);

                    FromPLC = new FromPLC();
                    ToPLC = new ToPLC();
                }
                catch (Exception ex)
                {
                    throw new Exception("Problem creating PLC", ex);
                }
            }

            public async Task<bool> ConnectAsync()
            {
                if (!NetworkCheck.IsNetworkAvailable())
                {
                    throw new Exception($"Unable to connect to PLC @ {_ip}. No Network");
                }

                var retryCnt = 0;
                var sw = new Stopwatch();
                var canPing = false;
                sw.Restart();



                while (sw.Elapsed < TimeSpan.FromMinutes(5))
                {
                    _logger.Warn($"Attempting to ping the PLC..{retryCnt}.");
                    canPing = NetworkCheck.PingHost(_ip);

                    if (canPing)
                    {
                        _logger.Warn("Ping Success!");
                        break;
                    }
                    await TaskEx.Delay(5000);
                    retryCnt += 1;

                }

                if (!canPing)
                {
                    throw new Exception($"Unable to connect to PLC @ {_ip}. Can't ping PLC");
                }

                var connectionResult = _plc.Open();

                if (connectionResult != ErrorCode.NoError)
                {
                    //MessageBox.Show("Connection Error");
                    throw new Exception($"Unable to connect to PLC @ {_ip}. Error code is {connectionResult.ToString()}");
                }
                else
                {
                    _scanning = true;
                    Start();
                    return IsConnected;
                }

            }


            public bool Connect()
            {

                if (!NetworkCheck.IsNetworkAvailable())
                {
                    throw new Exception($"Unable to connect to PLC @ {_ip}. No Network");
                }

                _logger.Warn("Attempting to ping the PLC...");

                var canPing = NetworkCheck.PingHost(_ip);

                _logger.Warn($"Can ping = {canPing}");

                if (!canPing)
                {
                    throw new Exception($"Unable to connect to PLC @ {_ip}. Can't ping PLC");
                }

                var connectionResult = _plc.Open();

                if (connectionResult != ErrorCode.NoError)
                {
                    //MessageBox.Show("Connection Error");
                    throw new Exception($"Unable to connect to PLC @ {_ip}. Error code is {connectionResult.ToString()}");
                }
                else
                {
                    _scanning = true;
                    Start();
                    return IsConnected;
                }


            }



            protected override void ExecutionCore(CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        //FromOkuma DB = 7;
                        //ToOkuma DB = 8;

                        //Console.WriteLine("tick");

                        ReadPLC();

                        WriteToPLC();

                    }
                    catch (Exception ex)
                    {
                        _errCnt += 1;
                        if (_errCnt > 10) { _scanning = false; }
                        EventAggregationProvider.MonitorToolsAggregator.PublishOnUIThread(new PLCError($"Problem trying to write to PLC: {_plc.LastErrorString}{Environment.NewLine}{ex.Message}"));
                    }
                }
            }

            void ReadPLC()
            {
                try
                {
                    //_sw1.Start();
                    //Console.WriteLine($"Starting Read");
                    _plc.ClearLastError();
                    FromPLC.FromPlcStruct = (FromPLCStruct)_plc.ReadStruct(typeof(FromPLCStruct), 8);
                    //Console.WriteLine($"Read Complete @ {_sw1.ElapsedMilliseconds}");
                    //_sw1.Reset();
                    if (_plc.LastErrorCode != ErrorCode.NoError)
                    {
                        throw new Exception($"While reading from PLC");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            void WriteToPLC()
            {
                try
                {
                    //_sw2.Start();
                    //Console.WriteLine($"Starting Write");

                    ToPLC.Watchdog = !ToPLC.Watchdog;

                    _plc.ClearLastError();
                    _plc.WriteStruct(ToPLC.ToPlcStruct, 7);
                    //Console.WriteLine($"Write Complete @ {_sw2.ElapsedMilliseconds}");
                    //_sw2.Reset();
                    if (_plc.LastErrorCode != ErrorCode.NoError)
                    {
                        throw new Exception($"While writing to PLC");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            int _errCnt = 0;

            public void Disconnect()
            {
                _scanning = false;
                System.Threading.Thread.Sleep(1000);
                _plc.Close();
            }

            public ToPLC ToPLC { get; set; }

            public FromPLC FromPLC { get; set; }

            public bool IsConnected => _plc != null ? _plc.IsConnected : false;
            public bool IsAvailable => _plc != null ? _plc.IsAvailable : false;

            public string IP => _ip;

            public void Dispose()
            {
                Stop();
                if (_plc != null)
                {
                    _plc.Close();
                    _plc.Dispose();
                }
            }

            public Task<bool> WaitForSignal(Func<bool> target, bool targetState, int timeout)
            {

                CancellationTokenSource cancelSource = new CancellationTokenSource();
                cancelSource.CancelAfter(timeout);
                var token = cancelSource.Token;

                try
                {

                    return Task<bool>.Factory.StartNew(() =>
                    {
                        //var fromPlc = (FromPLCStruct)_plc.ReadStruct(typeof(FromPLCStruct), 8);

                        bool tgt = target.Invoke();

                        //Console.WriteLine($"Wait for target {tgt} = {targetState}");

                        while (tgt != targetState)
                        {
                            //Console.WriteLine($"tgt = {tgt}");
                            if (token.IsCancellationRequested)
                            { return false; }
                            Thread.Sleep(300);
                            tgt = target.Invoke();
                        }
                        //Console.WriteLine($"Target = {targetState}");
                        return true;
                    }, token);
                }
                catch (OperationCanceledException ex)
                {
                    return Task<bool>.Factory.StartNew(() => false);
                }
            }


        }
    }
