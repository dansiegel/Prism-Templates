using System;
using Prism;
using Prism.Behaviors;
using Prism.Common;
using Xamarin.Forms;

namespace Company.MobileApp.Behaviors
{
    public class IsActiveAwareBehavior : BehaviorBase<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject.Appearing += OnAppearing;
            AssociatedObject.Disappearing += OnDisappearing;
        }
        
        protected override void OnDetachingFrom(Page bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject.Appearing -= OnAppearing;
            AssociatedObject.Disappearing -= OnDisappearing;
        }

        private void OnAppearing(object sender, EventArgs e) =>
            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(AssociatedObject, v => v.IsActive = true);
        
        private void OnDisappearing(object sender, EventArgs e) =>
            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(AssociatedObject, v => v.IsActive = false);
    }
}