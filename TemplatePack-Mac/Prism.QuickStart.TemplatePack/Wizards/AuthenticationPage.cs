using System;
using MonoDevelop.Ide.Templates;
using Prism.QuickStart.TemplatePack.Widgets;
using Xwt;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class AuthenticationPage : WizardPage
    {
        TemplateWizard _wizard { get; }

        public AuthenticationPage(TemplateWizard wizard)
        {
            _wizard = wizard;
        }

        public override string Title => "Authentication Settings";

        protected override object CreateNativeWidget<T>()
        {
            var widget = new AuthenticationSettingsWidget();
            return Toolkit.CurrentEngine.GetNativeWidget(widget);
        }
    }
}
