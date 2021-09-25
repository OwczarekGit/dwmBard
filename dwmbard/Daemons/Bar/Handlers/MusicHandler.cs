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
            
            var stdOut = status.Item1.Trim();
            //var stdErr = status.Item2.Trim();

            if (stdOut.ToLower().Contains("playing"))
            {
                returnValuePrefix = "";
            }
            else
            {
                returnValuePrefix = "";
            }

            string artist = CommandRunner.getCommandOutput(artistCommand).Trim();
            string title = CommandRunner.getCommandOutput(titleCommand).Trim();

            if (status.Equals(""))
            {
                returnValue = $"";
            }
            else
            {
                if (artist.Length + title.Length > maxTitleLength)
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
                    returnValue = $" {artist} - {title}".Replace('\'', '`').Replace('\"','`');
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
