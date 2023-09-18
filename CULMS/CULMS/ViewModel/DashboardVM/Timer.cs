using System.Threading;
using System;
using Xamarin.Forms;

namespace CULMS.ViewModel.DashboardVM
{
    public class Timer
    {
        private readonly TimeSpan _timeSpan;

        private readonly Action _callback;




        private static CancellationTokenSource _cancellationTokenSource;




        public Timer(TimeSpan timeSpan, Action callback)

        {

            _timeSpan = timeSpan;

            _callback = callback;

            _cancellationTokenSource = new CancellationTokenSource();

        }

        public void Start()

        {

            CancellationTokenSource cts = _cancellationTokenSource; // safe copy

            Device.StartTimer(_timeSpan, () =>

            {

                if (cts.IsCancellationRequested)

                {

                    return false;

                }

                _callback.Invoke();

                return true; //true to continuous, false to single use

            });

        }




        public void Stop()

        {
            //CancellationTokenSource cts = _cancellationTokenSource; // safe copy

            //Device.StartTimer(_timeSpan, () =>

            //{

            //    if (cts.IsCancellationRequested)

            //    {

            //        return false;

            //    }

            //    _callback.Invoke();

            //    return false; //true to continuous, false to single use

            //});
            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();

        }

    }
}
