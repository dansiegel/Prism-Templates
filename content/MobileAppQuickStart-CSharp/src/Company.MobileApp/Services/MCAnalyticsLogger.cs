using System;
using System.Collections.Generic;
using FFImageLoading.Helpers;
using Microsoft.Azure.Mobile.Analytics;
using Prism.Logging;

namespace Company.MobileApp.Services
{
    public class MCAnalyticsLogger : ILoggerFacade, IMiniLogger
    {
        public void Debug(string message)
        {
            Analytics.TrackEvent("Debug", new Dictionary<string, string>
            {
                { "logger", nameof(IMiniLogger) },
                { "message", message }
            });
        }

        public void Error(string errorMessage)
        {
            Analytics.TrackEvent("Error", new Dictionary<string, string>
            {
                { "logger", nameof(IMiniLogger) },
                { "message", errorMessage }
            });
        }

        public void Error(string errorMessage, Exception ex)
        {
            Analytics.TrackEvent("Error", new Dictionary<string, string>
            {
                { "logger", nameof(IMiniLogger) },
                { "message", errorMessage },
                { "errorType", ex.GetType().Name },
                { "error", ex.ToString() }
            });
        }

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