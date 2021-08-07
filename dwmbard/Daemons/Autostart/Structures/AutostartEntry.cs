using System;
using System.Diagnostics;
using System.IO;
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
            runner.Name = $"dwmbard_{processName}";
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
            int restartCount = 1;
            do
            {
                Logger.Logger.info($"Autostart entry: {processName} started for: {restartCount} time.");
                
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "/bin/bash";
                    process.StartInfo.Arguments = $"-c \"{processName}\"";

                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    
                    process.OutputDataReceived += (sender, args) =>
                    {
                        Logger.Logger.info($"{processName}: {args.Data}");
                    };

                    process.ErrorDataReceived += (sender, args) =>
                    {
                        Logger.Logger.error($"{processName}: {args.Data}");
                    };
                    
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
                
                /*var process = new Process
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
                process.WaitForExit();*/

                if (keepRunning)
                    Logger.Logger.error($"Autostart entry: {processName} exited!");
                    
                restartCount++;
            } while (keepRunning);
            
            Logger.Logger.info($"Autostart entry: {processName} exited as it should.");
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