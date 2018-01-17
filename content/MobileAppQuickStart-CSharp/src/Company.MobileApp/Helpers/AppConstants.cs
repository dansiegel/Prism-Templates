using System;

namespace Company.MobileApp.Helpers
{
    public static class AppConstants
    {
        // Put constants here that are not of a sensitive nature
#if (AADB2CAuth)

        // Azure AD B2C Coordinates
        public static readonly string RedirectUri = $"msal{Secrets.AuthClientId}://auth";

        public static readonly string[] Scopes = { $"https://{Secrets.B2CName}.onmicrosoft.com/demoapi/demo.read" };

        public static readonly string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Secrets.B2CName}.onmicrosoft.com/";
        public static readonly string Authority = $"{AuthorityBase}{Secrets.PolicySignUpSignIn}";
        public static readonly string AuthorityEditProfile = $"{AuthorityBase}{Secrets.PolicyEditProfile}";
        public static readonly string AuthorityPasswordReset = $"{AuthorityBase}{Secrets.PolicyResetPassword}";
#endif
#if (UseAppCenter)

        public static string AppCenterStart
        {
            get
            {
                string startup = string.Empty;
#if (IncludeiOS)

                if(Guid.TryParse(Secrets.AppCenter_iOS_Secret, out Guid iOSSecret))
                {
                    startup += $"ios={iOSSecret};";
                }
#endif
#if (IncludeAndroid)

                if(Guid.TryParse(Secrets.AppCenter_Android_Secret, out Guid AndroidSecret))
                {
                    startup += $"android={AndroidSecret};";
                }
#endif
#if (UWPSupported)

                if(Guid.TryParse(Secrets.AppCenter_UWP_Secret, out Guid UWPSecret))
                {
                    startup += $"uwp={UWPSecret};";
                }
#endif

                return startup;
            }
        }
#endif
    }
}
