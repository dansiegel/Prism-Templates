using System.Globalization;

namespace Company.MobileApp.i18n
{
    // Based on the Localization guide from Xamarin
    // See https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/ for more information
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}