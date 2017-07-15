using Prism.Logging;
using Microsoft.Azure.Mobile.Analytics;
using System.Collections.Generic;

namespace Company.MobileApp.Services
{
    public class MCAnalyticsLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Analytics.TrackEvent($"{category}", new Dictionary<string, string>
            {
                { "logger", nameof(ILoggerFacade) },
                { "priority", $"{priority}" },
                { "message", message }
            });
        }
    }
}