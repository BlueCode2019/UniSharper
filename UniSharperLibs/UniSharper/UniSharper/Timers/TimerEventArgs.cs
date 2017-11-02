using System;

namespace UniSharper.Timers
{
    public delegate void TimerCompletedEventHandler(object sender, TimerEventArgs e);

    public delegate void TimerPausedEventHandler(object sender, TimerEventArgs e);

    public delegate void TimerResetedEventHandler(object sender, TimerEventArgs e);

    public delegate void TimerResumedEventHandler(object sender, TimerEventArgs e);

    public delegate void TimerStartedEventHandler(object sender, TimerEventArgs e);

    public delegate void TimerStoppedEventHandler(object sender, TimerEventArgs e);

    public delegate void TimerTickingEventHandler(object sender, TimerEventArgs e);

    public class TimerEventArgs : EventArgs
    {
        #region Constructors

        public TimerEventArgs(ITimer timer)
            : base()
        {
            Timer = timer;
        }

        #endregion Constructors

        #region Properties

        public ITimer Timer
        {
            get;
            private set;
        }

        #endregion Properties
    }
}