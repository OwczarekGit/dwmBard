using System;
using System.Threading;

namespace dwmBard.Interfaces
{
    public abstract class IRefreshing
    {
        protected int refreshTimeMS;
        protected string returnValue = "";
        protected string returnValuePrefix = "";
        public bool isEnabled { get; protected set; } = true;

        public bool manualRefreshPossible { protected set; get; } = false;

        public void waitCycle()
        {
            Thread.Sleep(refreshTimeMS);
        }
        
        public void setPrefix(string prefix)
        {
            returnValuePrefix = prefix;
        }
        
        public string? getResult()
        {
            if (returnValue.Length > 0)
                return $"{returnValuePrefix}{returnValue}";

            return null;
        }
    }
}