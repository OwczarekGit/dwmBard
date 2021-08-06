using System;
using dwmBard.Helpers;

namespace dwmBard.Handlers
{
    public class Backlight
    {
        private string backlightName;
        private MinMaxCurrent values;
        
        public Backlight(string path)
        {
            backlightName = path;

            int tmpMax = (int)CommandRunner.getValueFromFile($"{backlightName}/max_brightness");
            int tmpNow = (int)CommandRunner.getValueFromFile($"{backlightName}/actual_brightness");
            values = new MinMaxCurrent(tmpNow, 0, tmpMax);
        }

        private void updateValues()
        {
            int tmpNow = (int)CommandRunner.getValueFromFile($"{backlightName}/actual_brightness");
            values.current = tmpNow;
        }

        public int getNowPercent()
        {
            updateValues();
            return values.getCurrentPercent();
        }
    }
}