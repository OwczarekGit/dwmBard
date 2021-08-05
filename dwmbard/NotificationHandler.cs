using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard
{
    public class NotificationHandler : IParallelWorker, IConfigurable
    {
        private string getStatusCommand = "dunstctl is-paused";
        private string pauseCommand     = "dunstctl set-paused true";
        private string unpauseCommand   = "dunstctl set-paused false";
        
        public NotificationHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
            returnValue = "";
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