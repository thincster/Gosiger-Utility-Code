using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace Okuma_Monitor_Tools.Events
{
  public  class ChangeShellVisibilityEvent
    {
       
        public Visibility Visibility { get; private set; }
        public bool OnTop { get; private set; }
        public bool ShowMain { get; private set; }
        public  ChangeShellVisibilityEvent(Visibility visibility, bool OnTop = false, bool ShowMain = false)
        {
           
            
            Visibility = visibility;
            this.OnTop = OnTop;
            this.ShowMain = ShowMain;
        }

     
    }
}
