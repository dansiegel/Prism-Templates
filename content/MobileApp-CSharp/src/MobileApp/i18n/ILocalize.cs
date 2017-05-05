using System.Globalization;

namespace MobileApp.i18n
{
    // Based on the Localization guid from Xamarin
    // See https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/ for more information
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}