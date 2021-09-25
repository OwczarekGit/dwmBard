using System;
using dwmBard.Daemons;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class MusicHandler : IParallelWorker, IConfigurable
    {
        private string titleCommand  = "playerctl metadata title";
        private string artistCommand = "playerctl metadata artist";
        private string statusCommand = "playerctl status";
        private bool isPlaying = false;
        private int maxTitleLength = 60;
        
        
        public MusicHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
        }

        public override void doWork()
        {
            var status = CommandRunner.getCommandOutputWithStdErr(statusCommand);

            if (status.stdOut.ToLower().Contains("playing"))
            {
                returnValuePrefix = "";
            }
            else
            {
                returnValuePrefix = "";
            }

            var artist = CommandRunner.getCommandOutputWithStdErr(artistCommand);
            var title = CommandRunner.getCommandOutputWithStdErr(titleCommand);

            if (status.Equals("") || status.exitCode != 0)
            {
                returnValue = $"";
            }
            else
            {
                if (artist.stdOut.Length + title.stdOut.Length > maxTitleLength)
                {
                    returnValue = $" {title}".Replace('\'', '`').Replace('\"', '`');
                    
                    if (returnValue.Length > maxTitleLength)
                    {
                        int toCut = Math.Abs(maxTitleLength - returnValue.Length);
                        returnValue = returnValue.Remove(returnValue.Length - toCut-1, toCut+1);
                        returnValue += "…";
                    }
                }
                else
                {
                    returnValue = $" {artist.stdOut} - {title.stdOut}".Replace('\'', '`').Replace('\"','`');
                }
            }

            GC.Collect();
        }
        
        void IConfigurable.configure()
        {
            var tmpLength = Bar.config.getConfigValue("MusicHandler.maxTitleLength");
            maxTitleLength = tmpLength != null ? int.Parse(tmpLength) : maxTitleLength;
        }
    }
}
