using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using wmExtender.Daemons.Bar.Structures;

namespace dwmBard.Helpers
{
    public class CommandRunner
    {
        public static string getCommandOutput(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };

            process.Start();
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();

            return result;
        }
        
        public static CommandOutput getCommandOutputWithStdErr(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardErrorEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                }
            };

            process.Start();
            process.WaitForExit();
            string stdOutResult = process.StandardOutput.ReadToEnd();
            string stdErrResult = process.StandardOutput.ReadToEnd();

            return new CommandOutput(stdOutResult, stdErrResult, process.ExitCode);
        }
        
        public static void runCommand(string command)
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "/bin/bash",
                            Arguments = $"-c \"{command}\"",
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                        }
                    };
        
                    process.Start();
                }

        public static float getValueFromFile(string path)
        {
            if (!File.Exists(path))
                return -1;

            string tmp = File.ReadAllText(path);
            return float.Parse(tmp);
        }

        public static string getStringFromFile(string path)
        {
            if (!File.Exists(path))
                return "";

            string tmp = File.ReadAllText(path);
            return tmp;
        }
    }
}