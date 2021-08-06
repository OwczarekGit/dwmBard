using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using dwmBard.Enums;
using dwmBard.Handlers;
using dwmBard.Helpers;
using dwmBard.Interfaces;
using wmExtender.Structures;

namespace dwmBard.Daemons
{
    public class Bar
    {
        public const string CONFIG_FILE = "dwmbard.conf";
        public static List<IParallelWorker> workers = new List<IParallelWorker>();
        public static Config config;
        public static string CONFIG_DIRECTORY_PATH { get; private set; }

        public static void start()
        {
            CONFIG_DIRECTORY_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";
            
            var unixSignal = new SignalHandler(Bar.workers);
            unixSignal.start();

            Console.WriteLine("Started!");
            IParallelWorker tmpWorker;

            tmpWorker = new MusicHandler((int) CommonTimeouts.FiveSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new NetworkHandler((int) CommonTimeouts.ThirtySeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new WeatherHandler((int) CommonTimeouts.ThirtyMinutes);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new NotificationHandler((int) CommonTimeouts.TenSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new MicrophoneHandler((int) CommonTimeouts.TenSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new SoundHandler((int) CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new PowerHandler((int) CommonTimeouts.FiveSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new BrightnessHandler((int) CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new DateHandler((int) CommonTimeouts.Minute);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new TimeHandler((int) CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            var configWatcher = new Thread(initWatcher);
            configWatcher.Start();

            reloadConfig(null, null);

            foreach (var worker in Bar.workers)
                worker.start();


            while (true)
            {
                cycleWorkers();
                Thread.Sleep((int) CommonTimeouts.Second);
            }
        }

        public static void cycleWorkers()
        {
            string composed = "";

            foreach (var worker in Bar.workers)
                if (worker.isEnabled)
                    composed += $"{worker.getResult()} | ";

            var converted = composed.Remove(composed.Length - 1, 1);

            //CommandRunner.getCommandOutput($"xsetroot -name \'{converted}\'");
            Console.WriteLine(converted);
        }

        public static void reloadConfig(object sender, FileSystemEventArgs e)
        {
            Bar.config = new Config($"{Bar.CONFIG_DIRECTORY_PATH}/{Bar.CONFIG_FILE}");

            foreach (var worker in Bar.workers)
                if (worker is IConfigurable)
                    (worker as IConfigurable).configure();

            foreach (var worker in Bar.workers)
                worker.doWork();
        }

        // TODO: For some reason it only works in IDE, why? idk. ¯\_(ツ)_/¯
        private static void initWatcher()
        {
            while (true)
            {
                var watcher = new FileSystemWatcher(Bar.CONFIG_DIRECTORY_PATH);
                watcher.Filter = Bar.CONFIG_FILE;
                watcher.EnableRaisingEvents = true;
                watcher.Changed += reloadConfig;
                Thread.Sleep((int) CommonTimeouts.Hour);
            }
        }
    }
}