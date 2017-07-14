using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Company.MobileApp.Helpers;
using Xamarin.Forms.Platform.Android;

namespace Company.MobileApp.Droid
{
    [Activity(Label = "@string/ApplicationName",
              //Name="com.prismtemplate.name.MainActivity",
              //Exported = true,
              Icon = "@mipmap/ic_launcher",
              Theme = "@style/MyTheme",
              //LaunchMode = LaunchMode.SingleTask, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

#if (UseAzureMobileClient)
            LoadApplication(new App(new AndroidInitializer(Application)));
#else
            LoadApplication(new App(new AndroidInitializer()));
#endif
        }
    }
}
