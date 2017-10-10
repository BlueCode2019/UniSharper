using System;

namespace UniSharper.Timers
{
    public class TimerListEventArgs : EventArgs
    {
        public TimerListEventArgs(ITimerList timerList)
            : base()
        {
            TimerList = timerList;
        }

        public ITimerList TimerList
        {
            get;
            private set;
        }
    }

    public delegate void TimersStartedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersPausedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersResumedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersStoppedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersResetedEventHandler(object sender, TimerListEventArgs e);
}