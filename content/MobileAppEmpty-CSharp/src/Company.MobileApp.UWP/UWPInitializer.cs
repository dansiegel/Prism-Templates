using System;
using System.Collections.Generic;
using System.Linq;
using Prism;
using Prism.Ioc;

namespace Company.MobileApp.UWP
{
    public class UWPInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
        }
    }
}