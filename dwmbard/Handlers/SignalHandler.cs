using System.Collections.Generic;
using System.Threading;
using dwmBard.Daemons;
using dwmBard.Interfaces;
using Mono.Unix;
using Mono.Unix.Native;

namespace dwmBard.Handlers
{
    // This one captures SIGUSR1 and when it receives one will perform bar refresh and update things like volume or brightness.
    public class SignalHandler : IParallelWorker
    {
        private List<IParallelWorker> targetWorkers;

        public SignalHandler(List<IParallelWorker> refreshTargets) : base(1)
        {
            targetWorkers = refreshTargets;
        }

        public override void doWork()
        {
            var signal = new UnixSignal(Signum.SIGUSR1);
            while (running)
            {
                if (signal.WaitOne(Timeout.Infinite, false))
                {
                    foreach (var worker in targetWorkers)
                        if (worker.manualRefreshPossible)
                            worker.doWork();
                    
                    Bar.cycleWorkers();
                }
            }
        }
    }
}