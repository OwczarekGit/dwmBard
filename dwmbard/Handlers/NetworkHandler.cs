using System;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class NetworkHandler : IParallelWorker, IConfigurable
    {
        public NetworkHandler(int refreshTimeMs) : base(refreshTimeMs){}

        public override void doWork()
        {
            var addresses = CommandRunner.getCommandOutput($"nmcli -p | grep \"inet4\" | sed 's/.*inet4 //g; s/\\/.*$//g'").Trim();

            // TODO: Check if it works correctly for multiple network cards.
            //var addressesSeparated = addresses.Split(' ');

            returnValue = $" {addresses}";
            GC.Collect();
        }
    }
}