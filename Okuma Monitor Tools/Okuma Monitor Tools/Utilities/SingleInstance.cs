using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Okuma_Monitor_Tools.Events;
using UtilityCode;

namespace Okuma_Monitor_Tools.Utilities
{
  public   class SingleInstance 
    {
        private IEventAggregator _events;
       
        const string UniqueMutexName = "b4838d10-eaa5-431c-bd4b-fca3645de31a";
        const string UniqueEventName = "e6062a25-6660-4403-9734-b69c999ea331";
        EventWaitHandle eventWaitHandle;
        Mutex mutex;

    public    void CheckSingleInstance(IEventAggregator events)
    {
        _events = events;
            bool isOwned;
            this.mutex = new Mutex(true, UniqueMutexName, out isOwned);
            this.eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, UniqueEventName);
            GC.KeepAlive(this.mutex);
            if (isOwned)
            {
                var thread = new Thread(() =>
                    {
                        while (this.eventWaitHandle.WaitOne())
                        {
                            _events.PublishOnUIThread(new ChangeShellVisibilityEvent(Visibility.Visible, true));

                        }
                    }

                );
                thread.IsBackground = true;
                thread.Start();
                return;
            }
            
            this.eventWaitHandle.Set();

            System.Windows.Application.Current.Shutdown();
        }

    }
}
