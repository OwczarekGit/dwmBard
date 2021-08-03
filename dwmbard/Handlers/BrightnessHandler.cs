using System.Collections.Generic;
using System.IO;
using System.Linq;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class BrightnessHandler : IParallelWorker, IConfigurable
    {
        public static string backlightDirectory = $"/sys/class/backlight";
        private List<Backlight> backlights = new List<Backlight>();
        
        public BrightnessHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            foreach (var backlight in Directory.GetDirectories(backlightDirectory))
            {
                backlights.Add(new Backlight(backlight));
            }
        }

        public override void doWork()
        {
            if (backlights.Count > 0)
            {
                returnValue = $" {backlights.First().getNowPercent()}%";
            }
            else
            {
                returnValue = "";
            }
        }
    }
}