using System.Threading;

namespace dwmBard.Interfaces
{
    public abstract class IParallelWorker : IRefreshing
    {
        protected Thread worker;
        protected bool running = false;

        public IParallelWorker(int refreshTimeMs)
        {
            refreshTimeMS = refreshTimeMs;
        }

        public void start()
        {
            worker = new Thread(cycle);
            running = true;
            worker.Start();
        }

        protected void stop()
        {
            running = false;
        }

        protected void cycle()
        {
            while (running)
            {
                if (isEnabled) 
                    doWork();
                
                waitCycle();
            }
        }
        
        public abstract void doWork();
    }
}