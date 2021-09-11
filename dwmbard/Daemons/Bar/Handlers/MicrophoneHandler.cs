using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class MicrophoneHandler : IParallelWorker 
    {
        private string microphoneStatusCommand = "pactl get-source-mute @DEFAULT_SOURCE@";
        
        public MicrophoneHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
            returnValue = String.Empty;
        }

        public override void doWork()
        {
            var result = CommandRunner.getCommandOutput(microphoneStatusCommand).Trim();

            if (result.ToLower().Contains("yes"))
            {
                //returnValuePrefix = "";
                returnValuePrefix = "";
            }
            else
            {
                //returnValuePrefix = "";
                returnValuePrefix = "";
            }
            
            GC.Collect();
        }
    }
}