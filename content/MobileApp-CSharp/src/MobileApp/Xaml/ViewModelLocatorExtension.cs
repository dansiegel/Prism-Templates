using System;
using Xamarin.Forms.Xaml;
using Prism.Mvvm;

namespace MobileApp.Xaml
{
    /// <summary>
    /// The ViewModelLocator is a replacement for Prism's built in attachable property.
    /// This XAML Markup Extension allows you to dynamically set the Binding Context using
    /// Prism's ViewModelLocationProvider for any View Element.
    /// </summary>
    /// <code>
    /// xmlns:xaml="clr-namespace:MobileApp.Xaml"
    /// x:Name="view"
    /// BindingContext="{xaml:ViewModelLocator View={x:Reference view}}"
    /// </code>
    public class ViewModelLocatorExtension : IMarkupExtension
    {
        /// <summary>
        /// The View Element that we are locating a ViewModel for
        /// </summary>
        public object View { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            object viewModel = null;
            ViewModelLocationProvider.AutoWireViewModelChanged(View, (v, vm) => viewModel = vm);
            return viewModel;
        }
    }
}