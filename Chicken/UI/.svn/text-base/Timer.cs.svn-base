using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace UI
{
    class Timer
    {   // all time is in milliseconds
        private int timeBetweenTicks;
        private int lastTickTime;
        private int startTime;
        private int totalTicks;
        private int currentTime;
        private bool timerStarted;
        public Timer(int _timeBetweenTicks, int _totalTicks)
        {
            timeBetweenTicks = _timeBetweenTicks;
            lastTickTime = 0;
            startTime = 0;
            currentTime = 0;
            timerStarted = false;
            totalTicks = _totalTicks;
        }
        public int getCurrentTime()
        {
            if (timerStarted)
            {
                return currentTime - startTime;
            }
            return 0;
        }
        public bool finishedTicking()
        {
            return totalTicks == 0;
        }
        public bool update(GameTime gameTime)
        {
            currentTime = (int)gameTime.TotalGameTime.TotalMilliseconds;
            if (!timerStarted)
            {
                startTime = currentTime;
                lastTickTime = currentTime;
                timerStarted = true;
            }
            else if (currentTime - lastTickTime > timeBetweenTicks)
            {
                lastTickTime = currentTime;
                if (totalTicks != 0)
                {
                    totalTicks--;
                    return true;
                }
            }
            return false;
    }
    }
}
