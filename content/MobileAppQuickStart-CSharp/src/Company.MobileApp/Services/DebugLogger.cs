using Prism.Logging;
using static System.Diagnostics.Debug;

namespace Company.MobileApp.Services
{
    public class DebugLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority) =>
            WriteLine($"{category} - {priority}: {message}");
    }
}