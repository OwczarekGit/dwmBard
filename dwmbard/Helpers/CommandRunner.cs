using System;
using System.Diagnostics;
using System.IO;

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
        
        public static (string, string) getCommandOutputWithStdErr(string command)
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
                    UseShellExecute = false,
                }
            };

            process.Start();
            process.WaitForExit();
            string stdOutResult = process.StandardOutput.ReadToEnd();
            string stdErrResult = process.StandardOutput.ReadToEnd();

            return (stdOutResult, stdErrResult);
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