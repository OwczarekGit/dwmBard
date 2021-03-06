using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using dwmBard.Daemons;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class TimeHandler : IParallelWorker, IConfigurable
    {
        public bool showSeconds = false;
        public bool timeFormat24 = true;
        
        DateTime now = DateTime.Now;
        string hour   = String.Empty;
        string minute = String.Empty;
        string second = String.Empty;


        public TimeHandler(int refreshTimeMS) : base(refreshTimeMS)
        {
            manualRefreshPossible = true;
        }
        
        public override void doWork()
        {
            now = DateTime.Now;
            hour = String.Empty;
            minute = now.ToString("mm");
            second = String.Empty;

            hour = now.ToString(timeFormat24 ? "HH" : "hh");

            if (showSeconds)
            {
                second = $":{now.ToString("ss")}";
            }

            if (timeFormat24)
            {
                returnValue = $" {hour}:{minute}{second}";
            }
            else
            {
                returnValue = $" {hour}:{minute}{second} {now.ToString("tt")}";
            }
        }

        void IConfigurable.configure()
        {
            var tmpFormat = Bar.config.getConfigValue("TimeHandler.timeformat24");
            timeFormat24 = tmpFormat != null ? bool.Parse(tmpFormat) : timeFormat24;
            
            var tmpSec = Bar.config.getConfigValue("TimeHandler.showseconds");
            showSeconds = tmpSec != null ? bool.Parse(tmpSec) : showSeconds;
        }
    }
}