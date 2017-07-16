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
using Microsoft.Identity.Client;
#endif
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Prism.Logging;
using Company.MobileApp.Helpers;

namespace Company.MobileApp.Auth
{
    public class LoginProvider : LoginProviderBase
    {
        private ILoggerFacade _logger { get; }

#if (AADAuth || AADB2CAuth)
        private IPublicClientApplication _clientApplication { get; }

        private UIParent _parent { get; }

        public LoginProvider(IPublicClientApplication clientApplication, UIParent parent, 
                             IAccountStore accountStore, ILoggerFacade logger)
#else
        public LoginProvider(IAccountStore accountStore, ILoggerFacade logger) 
#endif
            : base(accountStore)
        {
#if (AADAuth || AADB2CAuth)
            _clientApplication = clientApplication;
            _parent = parent;
#endif
            _logger = logger;
        }

        public override string AccountServiceName => "Company.MobileApp";

#if (AADB2CAuth)
        public override async Task<MobileServiceUser> LoginAsync(IMobileServiceClient client)
#else
        public override Task<MobileServiceUser> LoginAsync(IMobileServiceClient client)
#endif
        {
#if (AADB2CAuth)
            try
            {
                AuthenticationResult authenticationResult = 
                    await _clientApplication.AcquireTokenSilentAsync(
                        AppConstants.Scopes,
                        GetUserByPolicy(Secrets.PolicySignUpSignIn),
                        AppConstants.Authority,
                        false
                    );

                var payload = new JObject();
                if(authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.AccessToken))
                {
                    payload["access_token"] = authenticationResult.AccessToken;
                }

                client.CurrentUser = await client.LoginAsync(MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, payload);

                return client.CurrentUser;
            }
            catch(Exception ex)
            {
                _logger.Log(ex.ToString(), Category.Exception, Priority.High);
                throw;
            }
#else
            // TODO: Implement your login logic
            throw new NotImplementedException();
#endif
        }

#if (AADB2CAuth || AADAuth)
        private IUser GetUserByPolicy(string policy)
        {
            foreach(var user in _clientApplication.Users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if(userIdentifier.EndsWith(policy, StringComparison.OrdinalIgnoreCase)) return user;
            }

            return null;
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
#endif
    }
}