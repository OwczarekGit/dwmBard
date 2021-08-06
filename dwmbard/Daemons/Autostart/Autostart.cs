using System;

namespace dwmBard.Daemons
{
    public class Autostart
    {
        public const string AUTOSTART_FILE = "autostart";
        public static string AUTOSTART_DIRECTORY_PATH;
            
        public static void start()
        {
            Console.WriteLine("Autostart daemon started!"); 
            AUTOSTART_DIRECTORY_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";

            var x = new AutostartFile($"{AUTOSTART_DIRECTORY_PATH}/{AUTOSTART_FILE}");

        }
    }
}