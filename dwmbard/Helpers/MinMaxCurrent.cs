using System;

namespace dwmBard.Helpers
{
    public class MinMaxCurrent
    {
        public int min;
        public int max;
        public int current;
        
        public MinMaxCurrent(int current, int min ,int max)
        {
            this.min = min;
            this.max = max;
            this.current = current;
        }

        public int getCurrent()
        {
            return current;
        }

        public void setCurrent(int value)
        {
            if (value > max)
                current = max;
            else if (value < min)
                current = value;
            else
                current = value;
        }
    }
}