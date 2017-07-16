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

namespace Company.MobileApp.Views
{
    public class DynamicTabbedPage : TabbedPage, INavigatingAware
    {
#if (AutofacContainer || DryIocContainer)
        private IContainer _container { get; }

        public DynamicTabbedPage(IContainer container)
#endif
#if (NinjectContainer)
        private IKernel _kernel { get; }

        public DynamicTabbedPage(IKernel kernel)
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
            var tabs = parameters.GetValues<string>("tab");
            foreach(var tabSegment in tabs)
            {
                Page page = null;

                foreach(var segment in tabSegment.Split('/'))
                {
                    switch(page)
                    {
                        case null:
                            page = CreatePage(segment);
                            page.Behaviors.Add(new IsActiveAwareBehavior());
                            PageUtilities.OnNavigatingTo(page, parameters);
                            break;
                        case NavigationPage navPage:
                            var childPage = CreatePage(segment);
                            childPage.Behaviors.Add(new IsActiveAwareBehavior());
                            PageUtilities.OnNavigatingTo(childPage, parameters);
                            await navPage.PushAsync(childPage);
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
            var page = _kernel.Get<object>(name) as Page;
#endif

            if(ViewModelLocator.GetAutowireViewModel(page) == null)
            {
                ViewModelLocator.SetAutowireViewModel(page, true);
            }

            return page;
        }
    }
}