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

        public static string MobileCenterStart
        {
            get
            {
                string startup = string.Empty;

                if(Guid.TryParse(MobileCenter_iOS_Secret, out Guid iOSSecret))
                {
                    startup += $"ios={MobileCenter_iOS_Secret};";
                }

                if(Guid.TryParse(MobileCenter_Android_Secret, out Guid AndroidSecret))
                {
                    startup += $"android={MobileCenter_Android_Secret};";
                }

                if(Guid.TryParse(MobileCenter_UWP_Secret, out Guid UWPSecret))
                {
                    startup += $"uwp={MobileCenter_UWP_Secret};";
                }

                return startup;
            }
        }
#endif
    }
}
