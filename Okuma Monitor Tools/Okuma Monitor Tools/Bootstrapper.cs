using Caliburn.Micro;
using Okuma.Scout;
using Okuma_Monitor_Tools.Domain;
using Okuma_Monitor_Tools.Events;
using Okuma_Monitor_Tools.Okuma;
using Okuma_Monitor_Tools.Okuma.IOkumaAPI;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Okuma.OkumaMill;
using Okuma_Monitor_Tools.Testing;
using Okuma_Monitor_Tools.Utilities;
using Okuma_Monitor_Tools.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
//using ServiceStack;
using SimpleContainer = Caliburn.Micro.SimpleContainer;
using UtilityCode;


namespace Okuma_Monitor_Tools
{
    public class Bootstrapper: BootstrapperBase
    {
        // Create a logger for this class.
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
      
        SimpleContainer container;

        //  Declare an IEventAggregator.
        IEventAggregator _events;

        // Declare an IOkuma.
         IOkuma _genericMachine;

         // Declare an IOkumaMill.
         IOkumaMill _millAPI;

         // Declare an IOkumaLathe.
         IOkumaLathe _latheAPI;

         // Declare a ITimerService.
         ITimerService _tmrService;

        private ITestAPI _genericTestApi;
        private ITestLathe _testLathe;
        private ITestMill _testMill;

        public Bootstrapper()
        {
            // Newup an EventAggregator. 
            _events = new EventAggregator();

            // Newup a new TimerService pass in the EventAggregator
            _tmrService = new TimerService(_events);

            // Check to make sure the application is not already running.
            SingleInstance instance = new SingleInstance();
            instance.CheckSingleInstance(_events);
              
            Initialize();

           
        }

        //This is where we create our application level instances.
        protected override void Configure()
        {
            //Create a new instance of the container.
            container = new SimpleContainer();
           
            //Required in the override
            container.Singleton<IWindowManager, WindowManager>();

            //Register the Shell.
            container.PerRequest<IShell, ShellViewModel>();

            //Register the IEventAggregator
            container.RegisterInstance(typeof(IEventAggregator), "", _events);


            _genericTestApi = TestAPIFactory.GetTestApi();
            container.RegisterInstance(typeof(ITestAPI),"", _genericTestApi);
            _testLathe = TestAPIFactory.GetTestLathe();
            container.RegisterInstance(typeof(ITestLathe),"", _testLathe);
            _testMill = TestAPIFactory.GetTestMill();
            container.RegisterInstance(typeof(ITestMill),"",_testMill);

            _genericMachine = OkumaAPIFactory.GetAPI();
            container.RegisterInstance(typeof(IOkuma),"", _genericMachine);
            _latheAPI = OkumaAPIFactory.GetLatheAPI();
            container.RegisterInstance(typeof(IOkumaLathe),"",_latheAPI);
            _millAPI = OkumaAPIFactory.GetMillAPI();
            container.RegisterInstance(typeof(IOkumaMill),"",_millAPI);
           
          
            container.Singleton<IOkumaScanner, OkumaScanner>();

        }


        //Required boilerplate code for Dependency Injection.
        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        //Required boilerplate code for Dependency Injection.
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        //Required boilerplate code for Dependency Injection.
        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected async override void OnStartup(object sender, StartupEventArgs e)
        {
            if (My.Settings.Wizard == true)
            {
                //Display the Wizard
                DisplayRootViewFor<WizardViewModel>();
                
            }
            else
            {
                string _machineType = DMC.MachineType;
                string _okumaSerialNum = DMC.SerialNumber;

                if (_machineType == "L" || _machineType == "M" || _machineType == "G")

                    if (My.Settings.NetworkRequired == true)
                    {
                        // Check Network availability and Okuma IP is valid.
                        CheckNetwork();

                        //Check and set the Okuma IP Address on the machine.
                        CheckIP ip = new CheckIP(My.Settings.IPSubnet, My.Settings.OkumaCom);
                    }



                //Display the Shell
                DisplayRootViewFor<IShell>();

                //Start the TimerService
                _tmrService.Start();
               
            }
            
        }

        /// <summary>
        /// Checks to make sure the Network is available
        /// Checks the Okuma IP Address is valid
        /// </summary>
        private async void CheckNetwork()
        {
            // Check to see if the Network is ready.
            await NetworkCheck.WaitForNetwork(TimeSpan.FromMinutes(5));

            if (!NetworkCheck.IsNetworkAvailable())
            {
                //Logger.Error($"Failed to detect network after 5 minutes. Application will close ");

                MessageBox.Show("Network unavailable application will now close.");

                Environment.Exit(0);
            }
            
            string com1 = My.Settings.OkumaCom;
            string targetIp = $"{My.Settings.IPSubnet}";
            int okumaIP = 0;
           
            bool canconvert = false;

            if (string.IsNullOrWhiteSpace(com1) || com1.Length < 1 || com1.Length > 3)
            {
                MessageBox.Show($"OkumaCom {com1} has not been setup yet", "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Kill();
            }
            
            canconvert = int.TryParse(com1, out okumaIP);
            if (okumaIP < 1 && okumaIP > 254)
            {
                MessageBox.Show($"OkumaCom {com1} is out of range. Must be between 1 and 254", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Kill();
            }
        }


        /// <summary>
        /// This handles any exception that isn't caught by our exception handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //Logger.Fatal(e.Exception, "Unhandled Exception");
            e.Handled = true;
        }

        /// <summary>
        /// Kills this current process
        /// </summary>
        public static void Kill()
        {
            
            Process process = Process.GetCurrentProcess();
            process.Kill();
        }




       
   }
}
