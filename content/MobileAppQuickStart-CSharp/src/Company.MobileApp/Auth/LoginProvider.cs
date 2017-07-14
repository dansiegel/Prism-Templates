using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureMobileClient.Helpers;
using AzureMobileClient.Helpers.Accounts;
using Microsoft.WindowsAzure.MobileServices;

namespace Company.MobileApp.Auth
{
    public class LoginProvider : LoginProviderBase
    {
        public LoginProvider(IAccountStore accountStore) 
            : base(accountStore)
        {
        }

        public override string AccountServiceName => "Company.MobileApp";

        public override Task<MobileServiceUser> LoginAsync(IMobileServiceClient client)
        {
            // TODO: Implement your login logic
            throw new NotImplementedException();
        }
    }
}