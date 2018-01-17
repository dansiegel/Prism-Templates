﻿using System;
using System.Collections.Generic;
using System.Linq;
using Prism;
using Prism.Ioc;
#if (AutofacContainer)
using Autofac;
using Prism.Autofac;
#endif
#if (DryIocContainer)
using DryIoc;
using Prism.DryIoc;
#endif
#if (UnityContainer)
using Unity;
using Prism.Unity;
#endif

namespace Company.MobileApp.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
        }
    }
}
