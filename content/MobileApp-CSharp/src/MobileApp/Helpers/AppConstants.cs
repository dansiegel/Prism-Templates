using System;
namespace MobileApp.Helpers
{
    public static class AppConstants
    {
        public const string AppServiceEndpoint = "https://mobileapp.azurewebsites.net";
#if (UseAzureMobileClient)

        public const string AlternateLoginHost = "";

        public const string LoginUriPrefix = "";
#endif
#if (UseMobileCenter)

        public const string MobileCenter_iOS_Secret = "MOBILECENTER-IOS-SECRET";

        public const string MobileCenter_Android_Secret = "MOBILECENTER-ANDROID-SECRET";

        public const string MobileCenter_UWP_Secret = "MOBILECENTER-UWP-SECRET";
#endif
    }
}
