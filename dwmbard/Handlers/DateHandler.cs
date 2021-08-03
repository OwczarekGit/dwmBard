using System;
using dwmBard.Interfaces;

namespace dwmBard.Handlers
{
    public class DateHandler : IParallelWorker, IConfigurable
    {
        public char separator = '.';
        public bool showYear = true;
        
        DateTime today = DateTime.Now;
        private string day   = String.Empty;
        private string month = String.Empty;
        private string year  = String.Empty;
        
        public override void doWork()
        {
                today = DateTime.Now;
                
                day   = today.ToString("dd");
                month = today.ToString("MM");
                year  = showYear ? $"{separator}{today.ToString("yyyy")}" : String.Empty;
                
                returnValue = $" {day}{separator}{month}{year}";
        }

        public DateHandler(int refreshTimeMs) : base(refreshTimeMs)
        {
            manualRefreshPossible = true;
        }
    }
}