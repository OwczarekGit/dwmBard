using System;
using dwmBard.Enums;
using dwmBard.Helpers;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class SoundHandler : IParallelWorker
    {
        private uint audioLevel = 0;
        private bool isMuted    = false;

        public SoundHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
        }

        public override void doWork()
        {
            try
            {
                audioLevel = UInt32.Parse(CommandRunner.getCommandOutput("pamixer --get-volume"));
                isMuted = Boolean.Parse((ReadOnlySpan<char>) CommandRunner.getCommandOutput("pamixer --get-mute"));

                if (isMuted)
                {
                    returnValuePrefix = Volume.Muted.ToString();
                }
                else
                {
                    if (NumberUtilities.isInRange(audioLevel, 0, 33))
                    {
                        returnValuePrefix = Volume.Low.ToString();
                    }
                    else if (NumberUtilities.isInRange(audioLevel, 34, 66))
                    {
                        returnValuePrefix = Volume.Medium.ToString();
                    }
                    else if (NumberUtilities.isInRange(audioLevel, 67, 100))
                    {
                        returnValuePrefix = Volume.High.ToString();
                    }
                    else if (NumberUtilities.isInRange(audioLevel, 101, 149))
                    {
                        returnValuePrefix = Volume.Overamplified.ToString();
                    }
                    else
                    {
                        returnValuePrefix = Volume.OverTheTop.ToString();
                    }

                }

                returnValue = $" {audioLevel}%";
                GC.Collect();
            }catch{/* Sink doesn't exist error probably, it appears only on first cycle.*/}
        }
    }
}