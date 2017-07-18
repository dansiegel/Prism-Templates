using AzureMobileClient.Helpers;
using Company.MobileApp.Models;

namespace Company.MobileApp.Data
{
    public interface IAppDataContext
    {
#if (Empty)
        // TODO: Create ICloudSyncTable<Model> Models { get; }
#else
        ICloudSyncTable<TodoItem> TodoItems { get; }
#endif
    }
}