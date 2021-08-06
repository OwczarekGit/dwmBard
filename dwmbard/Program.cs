using dwmBard.Daemons;

namespace dwmBard
{
    class Program
    {
        static void Main(string[] args)
        {
            Autostart.start();
            Bar.start();
        }
    }
}