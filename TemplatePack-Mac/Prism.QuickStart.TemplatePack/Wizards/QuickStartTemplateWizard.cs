using System;
using MonoDevelop.Ide.Templates;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class QuickStartTemplateWizard : TemplateWizard
    {
        public override string Id => "Prism.QuickStart.ProjectTemplateWizard";

        public override WizardPage GetPage(int pageNumber)
        {
            return new PrismAppInfoPage(this);
        }
    }
}
