using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Caliburn.Micro;
using UtilityCode;

namespace Okuma_Monitor_Tools.Events
{
    public interface ITimerService : IDisposable
    {
        void Start();
        void Stop();

        bool TimerRunning { get; set; }
    }
    public class TimerService: ITimerService
    {
        private Timer _tmr;
        private DateTime _lastOneSecondTick;
        private DateTime _lastThirtySecondTick;
        private int _scanInterval;

        private IEventAggregator _events;

        /// <summary>
        /// TimerService Constructor.
        /// Used to define and publish the timers.
        /// </summary>
        /// <param name="events">Injects the IEventAggregator</param>
        public TimerService(IEventAggregator events)
        {
            _events = events;
            _scanInterval = My.Settings.ScanInterval;
            _lastOneSecondTick = DateTime.Now;
            _lastThirtySecondTick = DateTime.Now;
        }

        private void Callback(object state)
        {
            // Create a 1 second timer and publish it on the UI thread.
            if (DateTime.Now.Subtract(_lastOneSecondTick).TotalMilliseconds > 1000)
            {
                _events.PublishOnUIThread(new OneSecondUiTmrEvent());
                _lastOneSecondTick = DateTime.Now;
            }

            // Create a 30 second timer and publish it on the UI thread.
            if (DateTime.Now.Subtract(_lastThirtySecondTick).TotalMilliseconds > 30000)
            {
                _events.PublishOnUIThread(new ThirtySecondUiTmrEvent());
                _lastThirtySecondTick = DateTime.Now;
            }
            if (TimerRunning)
            {
                _tmr.Change(_scanInterval, Timeout.Infinite);
            }
        }

        public void Stop()
        {
            TimerRunning = false;
        }

        public void Start()
        {
            TimerRunning = true;
            _tmr = new Timer(Callback, null, _scanInterval, Timeout.Infinite);
        }

        public void Dispose()
        {
            _tmr.Dispose();
        }

        public bool TimerRunning { get; set; }
   }
}
