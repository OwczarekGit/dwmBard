using System;
using System.IO;
using System.Threading;
using dwmBard.Daemons;
using dwmBard.Enums;
using dwmBard.Handlers;
using dwmBard.Helpers;
using dwmBard.Interfaces;
using wmExtender.Structures;

namespace dwmBard
{
    class Program
    {
        static void Main(string[] args)
        {
            Bar.start();
        }
    }
}