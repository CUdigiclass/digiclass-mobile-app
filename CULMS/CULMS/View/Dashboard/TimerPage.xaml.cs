using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CULMS.View.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPage : ContentPage
    {
        public TimerPage()
        {
            InitializeComponent();

            StartCountDownTimer();
        }
        DateTime endTime = new DateTime(2021, 4, 17, 9, 0, 0);
        public void StartCountDownTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += t_Tick;
            TimeSpan ts = endTime - DateTime.Now;
            cTimer = ts.ToString("d' Days 'h' Hours 'm' Minutes 's' Seconds'");
            timer.Start();
        }
        string cTimer;
        System.Timers.Timer timer;
        void t_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = endTime - DateTime.Now;


            cTimer = ts.ToString("h':'m':'s' '");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Code to run on the main thread  
                mylabel.Text = cTimer;
            });

            if ((ts.TotalMilliseconds < 0) || (ts.TotalMilliseconds < 1000))
            {
                timer.Stop();
            }
        }
    }
}
