using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma_Monitor_Tools
{
  public  class EventAggregationProvider
    {
        public static EventAggregator MonitorToolsAggregator { get; set; } = new EventAggregator();

    }


}
