using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureMobileClient.Helpers;
using AzureMobileClient.Helpers.Accounts;
#if (AADAuth || AADB2CAuth)
using AzureMobileClient.Helpers.AzureActiveDirectory;
using Microsoft.Identity.Client;
#endif
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Prism.Logging;
using Company.MobileApp.Helpers;

namespace Company.MobileApp.Auth
{
#if (AADAuth || AADB2CAuth)
    public class LoginProvider : AADLoginProvider
#else
    public class LoginProvider : LoginProviderBase<MobileAppUser>
#endif
    {
        private ILoggerFacade _logger { get; }

#if (AADAuth || AADB2CAuth)
        public LoginProvider(IPublicClientApplication client, UIParent parent, 
                             IAADOptions options, ILoggerFacade logger)
            : base(client, parent, options)
#else
        public LoginProvider(IAccountStore accountStore, ILoggerFacade logger) 
            : base(accountStore)
#endif
        {
            _logger = logger;
        }

        public override string AccountServiceName => "Company.MobileApp";

#if (AADAuth || AADB2CAuth)
        protected override void Log(Exception exception)
        {
            _logger.Log(exception);
        }
#else
        public override Task<MobileServiceUser> LoginAsync(IMobileServiceClient client)
        {
            // TODO: Implement your login logic
            throw new NotImplementedException();
        }
#endif
    }
}