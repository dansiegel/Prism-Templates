using Prism.Logging;
using Microsoft.Azure.Mobile.Analytics;
using System.Collections.Generic;

namespace Company.MobileApp.Services
{
    public class MCAnalyticsLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Analytics.TrackEvent(category.ToString(), new Dictionary<string, string>
            {
                { "priority", $"{priority}" },
                { "message", message }
            });
        }
    }
}