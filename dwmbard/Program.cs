using System;
using System.Threading;
using dwmBard.Daemons;
using dwmBard.Enums;

namespace dwmBard
{
    class Program
    {
        static void Main(string[] args)
        {
            Autostart.start();
            Bar.start();

            while (true)
                Thread.Sleep((int)CommonTimeouts.Minute);
        }
    }
}