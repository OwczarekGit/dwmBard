using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class MusicHandler : IParallelWorker
    {
        private string titleCommand  = "playerctl metadata title";
        private string artistCommand = "playerctl metadata artist";
        private string statusCommand = "playerctl status";
        private bool isPlaying = false;
        
        
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
                returnValue = $"";
            }
            else
            {
                int maxLength = 60;
                if (artist.Length + title.Length > maxLength)
                {
                    returnValue = $" {title}".Replace('\'', '`').Replace('\"', '`');
                    
                    if (returnValue.Length > maxLength)
                    {
                        int toCut = Math.Abs(maxLength - returnValue.Length);
                        returnValue = returnValue.Remove(returnValue.Length - toCut-1, toCut+1);
                        returnValue += "…";
                    }
                }
                else
                {
                    returnValue = $" {artist} - {title}".Replace('\'', '`').Replace('\"','`');
                }
            }

            GC.Collect();
        }
    }
}
