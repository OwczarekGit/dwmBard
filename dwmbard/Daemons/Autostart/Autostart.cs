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
        
            
        public static void start()
        {
            Logger.Logger.warning("Autostart daemon started!"); 
            AUTOSTART_DIRECTORY_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";

            reloadAutostart();
            startProcesses();
        }

        private static void startProcesses()
        {
            foreach (var entry in autostart.autostartEntries)
                entry.start();
        }

        public static void reloadAutostart()
        {
            autostart = new AutostartFile($"{AUTOSTART_DIRECTORY_PATH}/{AUTOSTART_FILE}");
        }
    }
}