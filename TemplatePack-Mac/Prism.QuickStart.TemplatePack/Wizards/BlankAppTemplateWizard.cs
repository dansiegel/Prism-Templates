using System;
using MonoDevelop.Ide.Templates;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class BlankAppTemplateWizard : TemplateWizard
    {
        public override string Id => "Prism.QuickStart.BlankProjectTemplateWizard";

        public override WizardPage GetPage(int pageNumber)
        {
            return new PrismAppInfoPage(this, false);
        }

        public override int TotalPages => 1;
    }
}
