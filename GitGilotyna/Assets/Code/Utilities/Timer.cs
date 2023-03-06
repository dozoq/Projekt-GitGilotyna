using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Utilities
{
    public class Timer
    {
        private float  ActualTime     { get; set; }
        private float  CallRepeatTime { get; }
        private bool   IsRunning      { get; set; }
        private Action Callback       { get; set; }

        public Timer(float repeatTime, Action callback, bool isRunning = true)
        {
            Callback       = callback;
            CallRepeatTime = repeatTime;
            IsRunning      = isRunning;
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
            if (ActualTime >= CallRepeatTime)
            {
                Callback.Invoke();
                ActualTime -= CallRepeatTime;
            }
            
        }
    }
}