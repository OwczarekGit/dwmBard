using dwmBard.Helpers;

namespace dwmBard.Handlers
{
    public class Battery
    {
        private string batteryNumber;
        private MinMaxCurrent values = new MinMaxCurrent(0, 0, 100);

        public Battery(string name)
        {
            batteryNumber = name;
        }

        public int getLevel()
        {
            values.current = (int) CommandRunner.getValueFromFile($"{batteryNumber}/capacity");
            return values.getCurrent();
        }
    }
};