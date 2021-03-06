using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using dwmBard.Enums;
using dwmBard.Handlers;
using dwmBard.Helpers;
using dwmBard.Interfaces;
using wmExtender.Daemons.Bar.Handlers;
using wmExtender.Structures;

namespace dwmBard.Daemons
{
    class Bar
    {
        public const string CONFIG_FILE = "dwmbard.conf";
        public static List<IParallelWorker> handlers = new List<IParallelWorker>();
        public static Config config;
        public static string CONFIG_DIRECTORY_PATH { get; private set; }
        
        private static Thread worker;
        public static bool running = false;

        public static void start()
        {
            Logger.Logger.warning("Bar daemon started.");
            CONFIG_DIRECTORY_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";
            
            var unixSignal = new SignalHandler(handlers);
            unixSignal.start();

            IParallelWorker tmpWorker;

            tmpWorker = new MusicHandler((int) CommonTimeouts.FiveSeconds);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new NetworkHandler((int) CommonTimeouts.ThirtySeconds);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new WeatherHandler((int) CommonTimeouts.ThirtyMinutes);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new NotificationHandler((int) CommonTimeouts.TenSeconds);
            handlers.Add(tmpWorker);

            tmpWorker = new MicrophoneHandler((int) CommonTimeouts.TenSeconds);
            handlers.Add(tmpWorker);

            tmpWorker = new UpdateHandler((int) CommonTimeouts.Hour);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new SoundHandler((int) CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new PowerHandler((int) CommonTimeouts.FiveSeconds);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new BrightnessHandler((int) CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new DateHandler((int) CommonTimeouts.Minute);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            tmpWorker = new TimeHandler((int) CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            handlers.Add(tmpWorker);

            var configWatcher = new Thread(initWatcher);
            configWatcher.Start();

            worker = new Thread(begin);
            worker.Start();
        }

        public static void begin()
        {
            if (running)
                return;
            
            reloadConfig(null, null);
            
            foreach (var handler in handlers)
                handler.start();

            running = true;
            while (running)
            {
                cycleWorkers();
                Thread.Sleep((int) CommonTimeouts.Second);
            }
        }

        public static void cycleWorkers()
        {
            string composed = "";

            foreach (var handler in handlers)
                if (handler.isEnabled)
                {
                    var result = handler.getResult();

                    if (result != null)
                        composed += $"{handler.getResult()} | ";
                }

            int trimEndCount = 1;
            var converted = composed.Remove(composed.Length - trimEndCount, trimEndCount);

            CommandRunner.getCommandOutput($"xsetroot -name \'{converted}\'");
            //Console.WriteLine(converted);
        }

        public static void reloadConfig(object sender, FileSystemEventArgs e)
        {
            config = new Config($"{CONFIG_DIRECTORY_PATH}/{CONFIG_FILE}");

            foreach (var handler in handlers)
                if (handler is IConfigurable)
                    (handler as IConfigurable).configure();

            foreach (var handler in handlers)
                handler.doWork();
            
            Logger.Logger.info("Bar config reloaded.");
        }

        private static void initWatcher()
        {
            while (true)
            {
                var watcher = CommandRunner.getCommandOutput($"inotifywait -e close_write {CONFIG_DIRECTORY_PATH}/{CONFIG_FILE}");
                reloadConfig(null, null);
                Logger.Logger.info("Config changed.");
                Thread.Sleep((int) CommonTimeouts.Second);
            }
        }
    }
}