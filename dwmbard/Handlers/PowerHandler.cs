using System;
using System.Collections.Generic;
using System.IO;
using dwmBard.Enums;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class PowerHandler : IParallelWorker, IConfigurable
    {
        public static string powerDirectory = $"/sys/class/power_supply";
        private List<Battery> batteries = new List<Battery>();
        private AC acPower;
        private bool isPlugged = false;
        
        public PowerHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            foreach (var source in Directory.EnumerateDirectories(powerDirectory))
            {
                if (source.ToLower().Contains("bat"))
                    batteries.Add(new Battery(source));

                if (source.ToLower().Contains("ac"))
                    acPower = new AC(source);
            }
            
        }

        public override void doWork()
        {
            if(acPower != null) 
                isPlugged = acPower.isPlugged();
            else
            {
                isPlugged = true;
            }
            
            int sum = 0;
            foreach (var battery in batteries)
                sum += battery.getLevel();

            
            if (batteries.Count > 0)
            { 
                int chargePercent = sum / batteries.Count; 
                returnValue = $" {chargePercent}%";
                returnValuePrefix = definePrefix(chargePercent);
            }
            else
            {
                returnValuePrefix = Power.AC.ToString();
            }
        }

        private string definePrefix(int level)
        {
            if (isPlugged)
            {
                if (batteries.Count > 0)
                { 
                    return Power.AC.ToString();
                }
                else
                {
                    returnValue = "";
                    return "";
                }
            }
            else
            {
                if (NumberUtilities.isInRange(level, 0, 20))
                {
                    return Power.Empty.ToString();
                }
                else if (NumberUtilities.isInRange(level, 21, 40))
                {
                    return Power.Low.ToString();
                }
                else if (NumberUtilities.isInRange(level, 41, 60))
                {
                    return Power.Medium.ToString();
                }
                else if (NumberUtilities.isInRange(level, 61, 80))
                {
                    return Power.High.ToString();
                }
                else
                {
                    return Power.Full.ToString();
                }
            }
        }
    }
}