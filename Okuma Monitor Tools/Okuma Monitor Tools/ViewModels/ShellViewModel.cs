using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Okuma_Monitor_Tools.Domain;
using Okuma_Monitor_Tools.Events;
using Okuma_Monitor_Tools.Okuma.OkumaLathe;
using Okuma_Monitor_Tools.Testing;
using Okuma_Monitor_Tools.Utilities;
using UtilityCode;

namespace Okuma_Monitor_Tools.ViewModels
{
   public class ShellViewModel: Conductor<object>, IShell, IHandle<OneSecondUiTmrEvent>
    {
        private static  NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IEventAggregator _events;
       
        //private ITestAPI _test;
        //private ITestLathe _lathe;
        //private ITestMill _mill;
        public ShellViewModel(IEventAggregator events)
        {
            _events=events;
            //_test = test;
            //_lathe = testLathe;
            //_mill = testMill;
          _events.Subscribe(this);

         
        }


        public void Handle(OneSecondUiTmrEvent message)
        {
            //_test.CreateMessage("It Worked");
            //_lathe.CreateMessage("Lathe Worked");
            //_mill.CreateMessage("Test Mill Worked");

        }

        public void Handle(ChangeShellVisibilityEvent message)
        {
            //    // Todo Bring to top if needed

            //    // For testing.
            //MessageBox.Show("The ChangeShellVisibility event has been triggered");
        }
    }

}
