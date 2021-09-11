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
            returnValue = $"  {CommandRunner.getCommandOutput(updateCountCommand).Trim()}";
            GC.Collect();
        }

        public void configure()
        {
            var tmpCommand = dwmBard.Daemons.Bar.config.getConfigValue("UpdateHandler.updateCountCommand");
            updateCountCommand = tmpCommand != null ? tmpCommand : updateCountCommand;
        }
    }
}