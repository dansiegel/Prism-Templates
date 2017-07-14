using AzureMobileClient.Helpers;
using Company.MobileApp.Models;

namespace Company.MobileApp.Data
{
    public interface IAppDataContext
    {
        ICloudSyncTable<TodoItem> TodoItems { get; }
    }
}