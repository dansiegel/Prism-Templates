using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
#if (AutofacContainer)
using Autofac;
#elif (DryIocContainer)
using DryIoc;
#elif (NinjectContainer)
using Ninject;
#else
using Microsoft.Practices.Unity;
#endif
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismApp4.i18n
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private CultureInfo _ci { get; }

        public TranslateExtension()
        {
            _ci = (Application.Current as App).Container.Resolve<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resmgr = new ResourceManager(typeof(Resx.Resources));

            var translation = resmgr.GetString(Text, _ci);

            if (translation == null)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Key '{Text}' was not found in resources for culture '{_ci.Name}'.");
#endif
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
            }

            return translation;
        }
    }
}
