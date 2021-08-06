using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class NetworkHandler : IParallelWorker
    {
        public NetworkHandler(int refreshTimeMs) : base(refreshTimeMs){}

        public override void doWork()
        {
            var addresses = CommandRunner.getCommandOutput($"nmcli -p | grep \"inet4\" | sed 's/.*inet4 //g; s/\\/.*$//g'")
                .Trim().Replace("\n"," | "); 

            returnValue = $" {addresses}";
            GC.Collect();
        }
    }
}