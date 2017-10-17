using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#if (IncludeAndroid)
[assembly: InternalsVisibleTo("Company.MobileApp.Droid")]
#endif
#if (IncludeiOS)
[assembly: InternalsVisibleTo("Company.MobileApp.iOS")]
#endif
#if (UWPSupported)
[assembly: InternalsVisibleTo("Company.MobileApp.UWP")]
#endif