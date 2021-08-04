using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class MusicHandler : IParallelWorker, IConfigurable
    {
        private string titleCommand  = "playerctl metadata title";
        private string artistCommand = "playerctl metadata artist";
        private string statusCommand = "playerctl status";
        private bool   isPlaying     = false;
        
        
        public MusicHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
        }

        public override void doWork()
        {
            var status = CommandRunner.getCommandOutput(statusCommand).Trim();

            if (status.ToLower().Contains("playing"))
            {
                returnValuePrefix = "";
            }
            else
            {
                returnValuePrefix = "";
            }

            string artist = CommandRunner.getCommandOutput(artistCommand).Trim();
            string title = CommandRunner.getCommandOutput(titleCommand).Trim();

            if (status.Equals(""))
            {
                returnValue = $" Nothing";
            }
            else
            {
                returnValue = $" {artist} - {title}";
            }

            GC.Collect();
        }
    }
}