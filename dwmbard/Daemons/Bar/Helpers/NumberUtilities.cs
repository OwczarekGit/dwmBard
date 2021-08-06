namespace dwmBard.Helpers
{
    public static class NumberUtilities
    {
        public static bool isInRange(float number, float min, float max)
        {
            return number >= min && number <= max;
        }
    }
}