using System;
using Unity.VisualScripting;

namespace Code.Utilities
{
    public class Timer
    {
        private float  ActualTime     { get; set; }
        private float  CallRepeatTime { get; }
        private bool   IsRunning      { get; set; }
        private Action Callback       { get; set; }

        public Timer(float repeatTime, Action callback)
        {
            Callback    = callback;
            CallRepeatTime = repeatTime;
        }

        public virtual void Start()
        {
            IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
        }

        public virtual void Reset()
        {
            ActualTime = 0;
        }

        public virtual void Update(float timePassed)
        {
            if (!IsRunning) return;
            ActualTime += timePassed;
            if (ActualTime <= CallRepeatTime)
            {
                Callback.Invoke();
                ActualTime -= CallRepeatTime;
            }
            
        }
    }
}