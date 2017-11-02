using System;

namespace UniSharper.Timers
{
    public delegate void TimersPausedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersResetedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersResumedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersStartedEventHandler(object sender, TimerListEventArgs e);

    public delegate void TimersStoppedEventHandler(object sender, TimerListEventArgs e);

    public class TimerListEventArgs : EventArgs
    {
        #region Constructors

        public TimerListEventArgs(ITimerList timerList)
            : base()
        {
            TimerList = timerList;
        }

        #endregion Constructors

        #region Properties

        public ITimerList TimerList
        {
            get;
            private set;
        }

        #endregion Properties
    }
}