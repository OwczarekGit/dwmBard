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
            var result = CommandRunner.getCommandOutput($"curl -s \"{provider}/{location}?format={format}\" | cut -c 9-").Trim();

            returnValue = $"  {result}";
        }
    }
}