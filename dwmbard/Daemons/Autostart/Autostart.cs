using System;
using System.Threading;
using dwmBard.Enums;

namespace dwmBard.Daemons
{
    public class Autostart
    {
        public const string AUTOSTART_FILE = "autostart";
        public static string AUTOSTART_DIRECTORY_PATH;
        public static AutostartFile autostart;
        private static Thread worker;
        public static bool running = false;
        
            
        public static void start()
        {
            Console.WriteLine("Autostart daemon started!"); 
            AUTOSTART_DIRECTORY_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";

            reloadAutostart();

            worker = new Thread(begin);
            worker.Start();
        }

        private static void begin()
        {
            if (running)
                return;

            running = true;
            while (running)
            {
                foreach (var entry in autostart.autostartEntries)
                    entry.assureIsRunning();

                Thread.Sleep((int)CommonTimeouts.FiveSeconds);
            }
        }

        public static void reloadAutostart()
        {
            autostart = new AutostartFile($"{AUTOSTART_DIRECTORY_PATH}/{AUTOSTART_FILE}");
        }
    }
}