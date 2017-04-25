using System;
using Prism.Logging;

namespace PrismTemplate.Extensions
{
    public static class ILoggerFacadeExtensions
    {
        public static void Log(this ILoggerFacade logger, object message, Category category = Category.Debug) =>
            logger.Log(message.ToString(), category, Priority.None);

        public static void Log(this ILoggerFacade logger, Exception exception, Category category = Category.Exception) =>
            logger.Log(exception.ToString(), category, Priority.None);
    }
}
