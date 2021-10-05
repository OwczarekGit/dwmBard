using System;
using System.Collections.Generic;

namespace dwmBard
{
    public class ArgumentParser
    {
        public static List<BoolArgument> boolArguments = new List<BoolArgument>();
        public bool initSuccessed = true;

        public ArgumentParser(string[] arguments)
        {
            string arg = string.Empty;

            try
            {
                for (var i = 0; i < arguments.Length; i++)
                {
                    arg = arguments[i];

                    if (arg.StartsWith('-'))
                    {
                        var stripped = arg.Replace("-", "").Trim();

                        if (stripped.Equals("usage"))
                        {
                            usage();
                            return;
                        }

                        foreach (var boolArgument in boolArguments)
                        {
                            if (stripped.Equals(boolArgument.propertyName))
                            {
                                if (boolArgument.isToggleable())
                                {
                                    boolArgument.toggle();
                                }
                                else
                                {
                                    var nextArg = arguments[i + 1];
                                    boolArgument.propertyValue = bool.Parse(nextArg);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Logger.error($"Error processing argument: {arg}");
                usage();
            }
            
            //printValues();
        }

        public static void insertBoolArg(BoolArgument arg)
        {
            boolArguments.Add(arg);
            //Logger.Logger.info($"Initialized valid argument: {arg.propertyName}.");
        }

        public void printValues()
        {
            foreach (var argument in boolArguments)
                Logger.Logger.info($"[{argument.propertyName} = {argument.propertyValue}]");
                
        }

        public bool? getBoolValue(string propertyName)
        {
            foreach (var boolArgument in boolArguments)
                if (propertyName.Equals(boolArgument.propertyName))
                    return boolArgument.propertyValue;

            return null;
        }

        public void usage()
        {
            initSuccessed = false;

            Console.WriteLine("Usage: dwmBard [options]\n");

            Console.WriteLine("Options:");
            foreach (var boolArgument in boolArguments)
                Console.WriteLine($"  {boolArgument.getUsage()}");
        }
    }
}