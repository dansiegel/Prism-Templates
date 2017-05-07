using System;
namespace MobileApp.Helpers
{
    public static class AppConstants
    {
        public const string BackendUri = "https://mobileapp.azurewebsites.net";
#if (UseMobileCenter)

        public const string MobileCenter_iOS_Secret = "MOBILECENTER-IOS-SECRET";

        public const string MobileCenter_Android_Secret = "MOBILECENTER-ANDROID-SECRET";
#endif
    }
}
