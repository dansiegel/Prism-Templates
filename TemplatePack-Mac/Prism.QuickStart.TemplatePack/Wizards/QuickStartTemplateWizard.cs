using System;
using MonoDevelop.Ide.Templates;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class QuickStartTemplateWizard : TemplateWizard
    {
        public override string Id => "Prism.QuickStart.ProjectTemplateWizard";

        public override WizardPage GetPage(int pageNumber)
        {
            switch (pageNumber)
            {
                case 1: return new PrismAppInfoPage(this, true);
                case 2: return new AppCenterPage(this);
                default: return new AuthenticationPage(this);
            }
            //return new PrismAppInfoPage(this);
        }

        public override int TotalPages => 3;
    }
}
