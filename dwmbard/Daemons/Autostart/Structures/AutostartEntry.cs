using System;
using System.Diagnostics;
using System.Threading;
using dwmBard.Helpers;

namespace dwmBard.Daemons
{
    public class AutostartEntry
    {
        public string processName { get; private set; }
        public string pgrepName   { get; private set; }
        public bool keepRunning   { get; private set; }
        private bool running = false;
        private Thread runner;
        
        // Autostart entry structure.
        public AutostartEntry(string processName, bool keepRunning, string? pgrepName=null)
        {
            this.processName = processName;
            this.keepRunning = keepRunning;

            this.pgrepName = pgrepName ?? processName;
            runner = new Thread(assureIsRunning);
            runner.Name = processName;
        }

        public void start()
        {
            if (running)
                return;
            
            running = true;
            Logger.Logger.info($"Autostart entry: {processName} started, keepRunning is: {keepRunning}.");
            runner.Start();
        }

        /* TODO: Fix ↓
         If processName contains additional parameters and pgrepName
         isn't provided the isRunning check will be pgreping processName
         with the parameters.
        */
        private void assureIsRunning()
        {
            int restartCount = 0;
            do
            {
                Logger.Logger.info($"Autostart entry: {processName} started for: {restartCount} time.");
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{processName}\"",
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                    }
                };

                process.Start();
                process.WaitForExit();
                
                Logger.Logger.error($"Autostart entry: {processName} exited!");
                restartCount++;
            } while (keepRunning);
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