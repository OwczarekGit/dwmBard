using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using dwmBard.Enums;
using dwmBard.Handlers;
using dwmBard.Helpers;
using dwmBard.Interfaces;
using Mono.Unix;
using wmExtender.Structures;

namespace dwmBard
{
    class Program
    {
        public static List<IParallelWorker> workers = new List<IParallelWorker>();
        public static string CONFIG_DIRECTORY_PATH { get; private set; }
        public const string CONFIG_FILE = "dwmbard.conf";
        public static Config config;

        static void Main(string[] args)
        {
            CONFIG_DIRECTORY_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/dwmBard";
            
            var unixSignal = new SignalHandler(workers);
            unixSignal.start();

            Console.WriteLine("Started!");
            IParallelWorker tmpWorker;
            
            tmpWorker = new MusicHandler((int)CommonTimeouts.FiveSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new NetworkHandler((int)CommonTimeouts.ThirtySeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new WeatherHandler((int)CommonTimeouts.ThirtyMinutes);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);
            
            tmpWorker = new NotificationHandler((int)CommonTimeouts.TenSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);
            
            tmpWorker = new MicrophoneHandler((int)CommonTimeouts.TenSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new SoundHandler((int)CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);
            
            tmpWorker = new PowerHandler((int)CommonTimeouts.FiveSeconds);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);
            
            tmpWorker = new BrightnessHandler((int)CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new DateHandler((int)CommonTimeouts.Minute);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new TimeHandler((int)CommonTimeouts.Second);
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            var configWatcher = new Thread(initWatcher);
            configWatcher.Start();

            reloadConfig(null,null);

            foreach (var worker in workers)
                worker.start();
            

            while (true)
            {
                cycleWorkers();
                Thread.Sleep((int)CommonTimeouts.Second);
            }
        }

        public static void cycleWorkers()
        {
            string composed = "";

            foreach (var worker in workers)
                if (worker.isEnabled)
                    composed += $"{worker.getResult()} | ";

            var converted = composed.Remove(composed.Length - 1, 1);
            
            CommandRunner.getCommandOutput($"xsetroot -name \'{converted}\'");
            //Console.WriteLine(converted);
        }

        private static void initWatcher()
        {
            while (true)
            {
                var watcher = new FileSystemWatcher(CONFIG_DIRECTORY_PATH);
                watcher.Filter = CONFIG_FILE;
                watcher.EnableRaisingEvents = true;
                watcher.Changed += reloadConfig;
                Thread.Sleep((int) CommonTimeouts.Hour);
            }
        }
        
        public static void reloadConfig(object sender, FileSystemEventArgs e)
        {
            config = new Config($"{CONFIG_DIRECTORY_PATH}/{CONFIG_FILE}");
            
            foreach (var worker in workers)
                if (worker is IConfigurable)
                    (worker as IConfigurable).configure();

            foreach (var worker in workers)
                worker.doWork();
                
        }
    }
}