using AzureMobileClient.Helpers;
using MobileApp.Helpers;

namespace MobileApp.Data
{
    // This is only needed when you are using Authentication with the Azure Mobile Client
    public class MobileAppServiceContextOptions : IAzureCloudServiceOptions
    {
        public string AppServiceEndpoint => AppConstants.AppServiceEndpoint;
        
        public string AlternateLoginHost { get; }

        public string LoginUriPrefix { get; }
    }
}