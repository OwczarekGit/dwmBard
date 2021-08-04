using System;
using System.Collections.Generic;
using System.Threading;
using dwmBard.Enums;
using dwmBard.Handlers;
using dwmBard.Helpers;
using dwmBard.Interfaces;
using Mono.Unix;

namespace dwmBard
{
    class Program
    {
        public static List<IParallelWorker> workers = new List<IParallelWorker>();

        static void Main(string[] args)
        {
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
                composed += $"{worker.getResult()} | ";

            var converted = composed.Remove(composed.Length - 1, 1);
            
            CommandRunner.getCommandOutput($"xsetroot -name \'{converted}\'");
            //Console.WriteLine(res);
        }
    }
}