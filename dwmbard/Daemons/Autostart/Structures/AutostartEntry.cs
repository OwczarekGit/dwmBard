using System;
using dwmBard.Helpers;

namespace dwmBard.Daemons
{
    public class AutostartEntry
    {
        public string processName { get; private set; }
        public string pgrepName   { get; private set; }
        public bool keepRunning   { get; private set; }
        private bool startedOnce = false;
        
        // Autostart entry structure.
        public AutostartEntry(string processName, bool keepRunning, string? pgrepName=null)
        {
            this.processName = processName;
            this.keepRunning = keepRunning;

            this.pgrepName = pgrepName ?? processName;
        }

        /* TODO: Fix ↓
         If processName contains additional parameters and pgrepName
         isn't provided the isRunning check will be pgreping processName
         with the parameters.
        */
        public void assureIsRunning()
        {
            if ((!keepRunning || isRunning()) && startedOnce)
                return;
            
            startedOnce = true;
            CommandRunner.runCommand($"{processName}");
        }
        

        public bool isRunning()
        {
            return Int32.Parse(Helpers.CommandRunner.getCommandOutput($"pgrep {pgrepName} | wc -l").Trim()) > 0;
        }

        public string toString()
        {
            return $"[Command: {processName}, KeepRunning: {keepRunning}, ProcessName: {pgrepName}]";
        }
    }
}