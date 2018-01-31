using System;
using System.Collections.Generic;
using MonoDevelop.Ide.Templates;
using Xwt;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class PrismAppInfoPage : WizardPage
    {
        TemplateWizard wizard { get; }

        public PrismAppInfoPage(TemplateWizard wizard)
        {
            this.wizard = wizard;
        }

        public override string Title
        {
            get
            {
                string templateName = wizard.Parameters["TemplateName"];
                return $"{templateName}: Basic Info";
            }
        }

        protected override object CreateNativeWidget<T>()
        {
            var view = new Widgets.BasicAppInfoWidget();
            return Toolkit.CurrentEngine.GetNativeWidget(view);
        }
    }
}
