using System;
using System.IO;

namespace dwmBard.Logger
{
    public class Logger
    {
        public static string LOGGING_FILE;
        public static string LOGGING_FILE_DIRECTORY;
        private static StreamWriter writer;
        private static DateTime timeStarted;
        private static bool enableLogging = true;

        public static void start()
        {
            LOGGING_FILE_DIRECTORY = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";

            prepareLoggingFile();
            
            info("Logging started!");
        }

        private static bool prepareLoggingFile()
        {
            if (Directory.Exists($"{LOGGING_FILE_DIRECTORY}"))
                Directory.CreateDirectory($"{LOGGING_FILE_DIRECTORY}");
            
            timeStarted = DateTime.Now;

            LOGGING_FILE = $"{timeStarted.ToString("dd-MM-yyyy")}-{timeStarted.ToString("HH-mm-ss")}.log";
            
            File.Create($"{LOGGING_FILE_DIRECTORY}/{LOGGING_FILE}");

            Console.WriteLine($"Created log file in: {LOGGING_FILE_DIRECTORY}.");
            return true;
        }

        public static void error(string message)
        {
            log($"[{DateTime.Now.ToString("HH:mm:ss")}] Error: {message}");
        }
        
        public static void info(string message)
        {
            log($"[{DateTime.Now.ToString("HH:mm:ss")}] Info: {message}");
        }

        public static void warning(string message)
        {
            log($"[{DateTime.Now.ToString("HH:mm:ss")}] Warning: {message}");
        }

        private static void log(string message)
        {
            Console.WriteLine(message);
            if (!enableLogging)
                return;

            try
            { 
                writer = new StreamWriter($"{LOGGING_FILE_DIRECTORY}/{LOGGING_FILE}", true);
                writer.WriteLine(message);
                writer.Close();
            }
            catch (Exception e){/*Unable to log.*/}
        }
    }
}