using System;
using System.Collections.Generic;
using System.Threading;
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

            tmpWorker = new WeatherHandler(1000*60*10); // Every ten minutes
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new SoundHandler(1000); // Every second
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new DateHandler(1000 * 60); // Every minute
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);

            tmpWorker = new TimeHandler(1000); // Every second
            tmpWorker.setPrefix("");
            workers.Add(tmpWorker);
            
            foreach (var worker in workers)
                worker.start();

            while (true)
            {
                cycleWorkers();
                Thread.Sleep(1000);
            }
        }

        public static void cycleWorkers()
        {
            string composed = "";

            foreach (var worker in workers)
                composed += $"{worker.getResult()} | ";

            var converted = composed.Remove(composed.Length - 2, 2);
            CommandRunner.getCommandOutput($"xsetroot -name '{converted}'");
        }
    }
}