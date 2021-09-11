using System;
using dwmBard.Daemons;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class WeatherHandler : IParallelWorker, IConfigurable
    {
        private string provider = "wttr.in";
        private string location = "Warsaw";
        private string format   = "1";
        private bool tempInF    = false;
        
        public WeatherHandler(int refreshTimeMs) : base(refreshTimeMs){}

        public override void doWork()
        {
            string result;
            if (tempInF)
                result = CommandRunner.getCommandOutput($"curl -s \"{provider}/{location}?format={format}\\&u").Trim();
            else
                result = CommandRunner.getCommandOutput($"curl -s \"{provider}/{location}?format={format}").Trim();
            
            var value = CommandRunner.getCommandOutput($"echo {result} | sed 's/.*[+-]//g'").Trim();
            var isPositive = result.Contains('+');
            var sign = isPositive ? "" : "-";
            
            returnValue = $"  {sign}{value}";
            GC.Collect();
        }

        public void configure()
        {
            var tmpLocation = Bar.config.getConfigValue("WeatherHandler.location");
            location = tmpLocation != null ? tmpLocation : location;
            
            var tmpUnit = Bar.config.getConfigValue("WeatherHandler.displayinf");
            tempInF = tmpUnit != null ? bool.Parse(tmpUnit) : tempInF;
        }
    }
}