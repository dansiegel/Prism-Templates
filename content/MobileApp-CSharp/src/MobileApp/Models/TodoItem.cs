using System;
#if (UseRealm)
using Realms;
#endif
#if (UseAzureMobileClient)
using AzureMobileClient.Helpers;
#endif
#if (!UseRealm)
using PropertyChanged;
#endif

namespace MobileApp.Models
{
    #if (UseRealm)
    public class TodoItem : RealmObject
    #elseif (UseAzureMobileClient)
    [ImplementPropertyChanged]
    public class TodoItem : EntityData
    #else
    [ImplementPropertyChanged]
    public class TodoItem
    #endif
    {
        public string Name { get; set; }

        public bool Done { get; set; }
    }
}