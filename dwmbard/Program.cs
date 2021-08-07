using System.Threading;
using dwmBard.Daemons;
using dwmBard.Enums;
using dwmBard.Logger;

namespace dwmBard
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Logger.start();
         
            Logger.Logger.info("Program started.");
            
            Autostart.start();
            Bar.start();

            Logger.Logger.info("Main thread goes into sleep mode.");
            while (true)
                Thread.Sleep((int)CommonTimeouts.Minute);
            
            Logger.Logger.error("This should never happen.");
        }
    }
}