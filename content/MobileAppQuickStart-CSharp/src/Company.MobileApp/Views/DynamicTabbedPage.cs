#if (AutofacContainer)
using Autofac;
#endif
#if (DryIocContainer)
using DryIoc;
#endif
#if (NinjectContainer)
using Ninject;
#endif
#if (UnityContainer)
using Microsoft.Practices.Unity;
#endif
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Company.MobileApp.Behaviors;
using Company.MobileApp.Helpers;

namespace Company.MobileApp.Views
{
    // NOTE: This class is being provided to better assist app development with Tabbed Pages until this feature is
    // included out of the box in the Navigation Service with Prism 7
    public class DynamicTabbedPage : TabbedPage, INavigatingAware
    {
#if (AutofacContainer)
        private IComponentContext _container { get; }

        public DynamicTabbedPage(IComponentContext container)
#endif
#if (DryIocContainer)
        private IContainer _container { get; }

        public DynamicTabbedPage(IContainer container)
#endif
#if (NinjectContainer)
        private IReadOnlyKernel _kernel { get; }

        public DynamicTabbedPage(IReadOnlyKernel kernel)
#endif
#if (UnityContainer)
        private IUnityContainer _container { get; }

        public DynamicTabbedPage(IUnityContainer container)
#endif
        {
#if (NinjectContainer)
            _kernel = kernel;
#else
            _container = container;
#endif
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            var tabs = parameters.GetValues<string>(AppConstants.DynamicTabKey);
            foreach(var tabSegment in tabs)
            {
                Page page = null;

                var uri = UriParsingHelper.Parse(tabSegment);
                foreach(var segment in UriParsingHelper.GetUriSegments(uri))
                {
                    if(page != null && !(page is NavigationPage)) continue;

                    var segmentPage = CreatePage(UriParsingHelper.GetSegmentName(segment));
                    segmentPage.Behaviors.Add(new IsActiveAwareBehavior());
                    PageUtilities.OnNavigatingTo(segmentPage, 
                                                 UriParsingHelper.GetSegmentParameters(segment, parameters));
                    switch(page)
                    {
                        case null:
                            page = segmentPage;
                            break;
                        case NavigationPage navPage:
                            await navPage.PushAsync(segmentPage);
                            break;
                    }
                }

                Children.Add(page);
            }
        }

        protected virtual Page CreatePage(string childName)
        {
#if (AutofacContainer)
            if (!_container.IsRegisteredWithName<Page>(childName))
                throw new NullReferenceException($"The requested page '{childName}' has not been registered.");

            var page = _container.ResolveNamed<Page>(childName);
#endif
#if (DryIocContainer || UnityContainer)
            var page = _container.Resolve<object>(childName) as Page;
#endif
#if (NinjectContainer)
            var page = _kernel.Get<object>(childName) as Page;
#endif

            if(ViewModelLocator.GetAutowireViewModel(page) == null)
            {
                ViewModelLocator.SetAutowireViewModel(page, true);
            }

            return page;
        }
    }
}