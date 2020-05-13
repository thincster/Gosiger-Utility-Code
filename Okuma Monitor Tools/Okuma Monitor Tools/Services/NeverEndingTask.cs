using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Caliburn.Micro;
using UtilityCode;

namespace Okuma_Monitor_Tools.Events
{
   
    public abstract class NeverEndingTask
    {
        // Using a CTS allows NeverEndingTask to "cancel itself"
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        private string _scanningTarget;
        protected NeverEndingTask(string scanningTarget)
        {
            
            _scanningTarget = scanningTarget;

            TheNeverEndingTask = new Task(
               () =>
               {
                   // Wait to see if we get cancelled...
                   while (!_cts.Token.WaitHandle.WaitOne(ExecutionLoopDelayMs))
                   {
                       // Otherwise execute our code...
                       ExecutionCore(_cts.Token);
                   }
                   // If we were cancelled, use the idiomatic way to terminate task
                   _cts.Token.ThrowIfCancellationRequested();
               },
               _cts.Token, TaskCreationOptions.LongRunning);

            // Do not forget to observe faulted tasks - for NeverEndingTask faults are probably never desirable
            TheNeverEndingTask.ContinueWith(x =>
            {
                Trace.TraceError(x.Exception.InnerException.Message);
                EventAggregationProvider.MonitorToolsAggregator.BeginPublishOnUIThread(new TimerErrorEvent($"Problem in ne task for {_scanningTarget}: {x.Exception.InnerException.Message}", x.Exception.InnerException));
            }, TaskContinuationOptions.OnlyOnFaulted);

        }

        protected readonly int ExecutionLoopDelayMs = My.Settings.ScanInterval;
        protected Task TheNeverEndingTask;

        public void Start()
        {
            // Should throw if you try to start twice...
            TheNeverEndingTask.Start();
        }

        protected abstract void ExecutionCore(CancellationToken cancellationToken);

        public void Stop()
        {
            // This code should be reentrant...
            _cts.Cancel();
            TheNeverEndingTask.Wait();
        }

    }



    public abstract class STANeverEndingTask
    {
        // Using a CTS allows NeverEndingTask to "cancel itself"
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private IEventAggregator _events;
        private string _scanningTarget;
        protected STANeverEndingTask(IEventAggregator events, string scanningTarget)
        {
            _events = events;
            _scanningTarget = scanningTarget;

            TheNeverEndingTask = StartSTATask(
               () =>
               {
                   // Wait to see if we get cancelled...
                   while (!_cts.Token.WaitHandle.WaitOne(ExecutionLoopDelayMs))
                   {
                       //Console.WriteLine(DateTime.Now.Millisecond.ToString());
                       // Otherwise execute our code...
                       ExecutionCore(_cts.Token);
                   }
                   // If we were cancelled, use the idiomatic way to terminate task
                   _cts.Token.ThrowIfCancellationRequested();
               },
               _cts.Token);

            // Do not forget to observe faulted tasks - for NeverEndingTask faults are probably never desirable
            TheNeverEndingTask.ContinueWith(x =>
            {
                Trace.TraceError(x.Exception.InnerException.Message);
                _events.BeginPublishOnUIThread(new TimerErrorEvent($"Problem in ne task for {_scanningTarget}: {x.Exception.InnerException.Message}", x.Exception));
            }, TaskContinuationOptions.OnlyOnFaulted);

        }

        protected readonly int ExecutionLoopDelayMs = My.Settings.ScanInterval;
        protected Task TheNeverEndingTask;

        public void Start()
        {
            // Should throw if you try to start twice...
            //TheNeverEndingTask.Start();
        }

        protected abstract void ExecutionCore(CancellationToken cancellationToken);

        public void Stop()
        {
            // This code should be reentrant...
            _cts.Cancel();
            TheNeverEndingTask.Wait();
        }

        public static Task StartSTATask(System.Action func, CancellationToken token)
        {
            //https://stackoverflow.com/a/35351613/245181
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    func();
                    tcs.SetResult(null);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }

}
