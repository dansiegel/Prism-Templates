using System.Collections.Generic;
using AzureMobileClient.Helpers.AzureActiveDirectory;
using Company.MobileApp.Helpers;

namespace Company.MobileApp.Auth
{
    public class AADOptions : IAADOptions, IAADLoginProviderOptions
    {
        public string RedirectUri => AppConstants.RedirectUri;

        public string Authority => AppConstants.Authority;

        public IEnumerable<string> Scopes => AppConstants.Scopes;

        public string Policy => Secrets.PolicySignUpSignIn;

        public string DirectoryName => Secrets.B2CName;
    }
}
