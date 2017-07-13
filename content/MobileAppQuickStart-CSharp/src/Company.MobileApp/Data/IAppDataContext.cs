using AzureMobileClient.Helpers;
using MobileApp.Models;

namespace MobileApp.Data
{
    public interface IAppDataContext
    {
        ICloudSyncTable<TodoItem> TodoItems { get; }
    }
}