using System;
using System.Collections.Generic;
using System.Threading;
using dwmBard.Daemons;
using dwmBard.Enums;
using dwmBard.Logger;

namespace dwmBard
{
    class Program
    {
        public static List<BoolArgument> validBoolArguments = new List<BoolArgument>()
        {
            new BoolArgument("nostartup", false, "Disable applications autostart, useful when starting form existing session"),
            new BoolArgument("noautostart", false, "Disable applications autostart, useful when starting form existing session"),
            new BoolArgument("nologs", false, "Disable logging to file"),
        };

        static void Main(string[] args)
        {
            foreach (var validBoolArgument in validBoolArguments)
                ArgumentParser.insertBoolArg(validBoolArgument);
            
            var parser = new ArgumentParser(args);

            if (!parser.initSuccessed)
                return;
            
            Logger.Logger.info("Program started.");

            if ( !(parser.getBoolValue("nologs") ?? false) )
                Logger.Logger.start();
            else
            {
                Logger.Logger.warning("Logging to file disabled!");
            }

            if ( !(parser.getBoolValue("nostartup") ?? false) && !(parser.getBoolValue("noautostart") ?? false) )
                Autostart.start();
            else
            {
                Logger.Logger.warning("Running without autostart!");
            }
                
            Bar.start();

            Logger.Logger.info("Main thread goes into sleep mode.");
            while (true)
                Thread.Sleep((int)CommonTimeouts.Minute);
            
            Logger.Logger.error("This should never happen.");
        }
    }
}