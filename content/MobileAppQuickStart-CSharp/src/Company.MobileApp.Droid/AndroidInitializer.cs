﻿using System;
using System.Collections.Generic;
using System.Linq;
using Prism;
using Prism.Ioc;
#if (UseAzureMobileClient || AADAuth || AADB2CAuth)
using Android.App;
#endif
#if (AADAuth || AADB2CAuth)
using Microsoft.Identity.Client;
#endif

namespace Company.MobileApp.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
#if (UseAzureMobileClient)
        private Application CurrentApplication { get; }

        public AndroidInitializer(Application application)
        {
            CurrentApplication = application;
        }

#endif
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
#if (AADAuth || AADB2CAuth) 
            containerRegistry.RegisterInstance(new UIParent(Xamarin.Forms.Forms.Context as Activity));
#endif
        }
    }
}
