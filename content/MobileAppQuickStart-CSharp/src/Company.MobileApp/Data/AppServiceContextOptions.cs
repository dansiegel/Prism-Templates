using AzureMobileClient.Helpers;
using Company.MobileApp.Helpers;

namespace Company.MobileApp.Data
{
    // This is only needed when you are using Authentication with the Azure Mobile Client
    public class AppServiceContextOptions : IAzureCloudServiceOptions
    {
        public string AppServiceEndpoint => Secrets.AppServiceEndpoint;
        
        public string AlternateLoginHost => Secrets.AlternateLoginHost;

        public string LoginUriPrefix => Secrets.LoginUriPrefix;
    }
}