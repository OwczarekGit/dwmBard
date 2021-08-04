using System;
using System.Linq;
using System.Text.RegularExpressions;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class WeatherHandler : IParallelWorker, IConfigurable
    {
        private string provider = "wttr.in";
        private string location = "Warsaw";
        private string format = "1";
        
        public WeatherHandler(int refreshTimeMs) : base(refreshTimeMs){}

        public override void doWork()
        {
            var result = CommandRunner.getCommandOutput($"curl -s \"{provider}/{location}?format={format}").Trim();
            var value = CommandRunner.getCommandOutput($"echo {result} | sed 's/.*[+-]//g'").Trim();
            var isPositive = result.Contains('+');
            var sign = isPositive ? "+" : "-";
            
            returnValue = $"  {sign}{value}";
            GC.Collect();
        }
    }
}