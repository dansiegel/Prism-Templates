using System;
using MonoDevelop.Ide.Templates;
using Prism.QuickStart.TemplatePack.Widgets;
using Xwt;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class AppCenterPage : WizardPage
    {
        TemplateWizard _wizard { get; }

        public AppCenterPage(TemplateWizard wizard)
        {
            _wizard = wizard;
        }

        public override string Title => "AppCenter Options";

        protected override object CreateNativeWidget<T>()
        {
            var widget = new AppCenterSettingsWidget();
            return Toolkit.CurrentEngine.GetNativeWidget(widget);
        }
    }
}
