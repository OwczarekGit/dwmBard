using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace wmExtender.Daemons.Bar.Handlers
{
    public class UpdateHandler : IParallelWorker, IConfigurable
    {
        private string updateCountCommand = "pacman -Sup | wc -l";
        
        public UpdateHandler(int refreshTimeMs) : base(refreshTimeMs){}

        public override void doWork()
        {
            string result = CommandRunner.getCommandOutput(updateCountCommand).Trim();
            
            if (result != "0")
                returnValue = $"  {result}";
            else
                returnValue = String.Empty;
                
            GC.Collect();
        }

        public void configure()
        {
            var tmpCommand = dwmBard.Daemons.Bar.config.getConfigValue("UpdateHandler.updateCountCommand");
            updateCountCommand = tmpCommand != null ? tmpCommand : updateCountCommand;
        }
    }
}