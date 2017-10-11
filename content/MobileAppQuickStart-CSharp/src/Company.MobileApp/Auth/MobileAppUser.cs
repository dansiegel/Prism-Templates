using System;
using System.Collections.Generic;
using System.Linq;
#if (AADAuth || AADB2CAuth)
using AzureMobileClient.Helpers.AzureActiveDirectory;
#else
using AzureMobileClient.Helpers.Accounts.OAuth;
#endif

namespace Company.MobileApp.Auth
{
#if (AADAuth || AADB2CAuth)
    public class MobileAppUser : AADAccount
#else
    public class MobileAppUser : OAuth2Account
#endif
    {
        // You can place any custom fields here
    }
}