using Company.MobileApp.i18n;

namespace Company.MobileApp.UWP.i18n
{
    // Universal Windows Platform automatically sets the resource's culture correctly.
    public class Localize : ILocalize
    {
        public void SetLocale (CultureInfo ci) { }
        public System.Globalization.CultureInfo GetCurrentCultureInfo ()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
    }
}