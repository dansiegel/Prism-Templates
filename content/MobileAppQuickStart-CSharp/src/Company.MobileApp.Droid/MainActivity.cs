using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Company.MobileApp.Helpers;
#if (AADAuth || AADB2CAuth)
using Microsoft.Identity.Client;
#endif
using Xamarin.Forms.Platform.Android;

namespace Company.MobileApp.Droid
{
    [Activity(Label = "@string/ApplicationName",
              Icon = "@mipmap/ic_launcher",
              Theme = "@style/MyTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
#if (IncludeBarcodeService)
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
#endif

#if (UseAzureMobileClient)
            LoadApplication(new App(new AndroidInitializer(Application)));
#else
            LoadApplication(new App(new AndroidInitializer()));
#endif
        }
#if (IncludeBarcodeService)

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
                                                    Permission[] grantResults)
    {
        global::ZXing.Net.Mobile
                         .Android
                         .PermissionsHandler
                         .OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }
#endif
#if (AADAuth || AADB2CAuth)

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
#endif
    }
}
