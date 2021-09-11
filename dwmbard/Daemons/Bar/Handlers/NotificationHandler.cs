using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard
{
    public class NotificationHandler : IParallelWorker
    {
        private string getStatusCommand = "dunstctl is-paused";
        private string pauseCommand     = "dunstctl set-paused true";
        private string unpauseCommand   = "dunstctl set-paused false";
        
        public NotificationHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
            returnValue = String.Empty;
        }

        public override void doWork()
        {
            
            var statusPaused = isPaused();

            if (statusPaused)
            {
                returnValuePrefix = "";
            }
            else
            {
                returnValuePrefix = "";
            }
        }

        private bool isPaused()
        {
            var result = CommandRunner.getCommandOutput(getStatusCommand).Trim();
            return result.Contains("true");
        }
    }
}