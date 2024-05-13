namespace KnightsTale.Models
{
    public class Timer
    {
        public bool goodToGo;
        protected int mSec;
        protected TimeSpan timer = new();


        public Timer(int mS)
        {
            goodToGo = false;
            mSec = mS;
        }
        public Timer(int mS, bool startLoaded)
        {
            goodToGo = startLoaded;
            mSec = mS;
        }

        public int MSec
        {
            get { return mSec; }
            set { mSec = value; }
        }
        public int MsTimer
        {
            get { return (int)timer.TotalMilliseconds; }
        }

        public void UpdateTimer()
        {
            timer += Globals.GameTime.ElapsedGameTime;
        }

        public void UpdateTimerSpeed(float Speed)
        {
            timer += TimeSpan.FromTicks((long)(Globals.GameTime.ElapsedGameTime.Ticks * Speed));
        }

        public virtual void AddTimeToTimer(int MSEC)
        {
            timer += TimeSpan.FromMilliseconds((long)(MSEC));
        }

        public bool Check()
        {
            if (timer.TotalMilliseconds >= mSec || goodToGo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, mSec / 60000, mSec / 1000, mSec % 1000));
            if (timer.TotalMilliseconds < 0)
            {
                timer = TimeSpan.Zero;
            }
            goodToGo = false;
        }

        public void ResetAndCreateNewTimer(int newTImer)
        {
            timer = TimeSpan.Zero;
            MSec = newTImer;
            goodToGo = false;
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            goodToGo = false;
        }

        public void SetTimer(TimeSpan time)
        {
            timer = time;
        }

        public virtual void SetTimer(int msec)
        {
            timer = TimeSpan.FromMilliseconds((long)(msec));
        }
    }
}

