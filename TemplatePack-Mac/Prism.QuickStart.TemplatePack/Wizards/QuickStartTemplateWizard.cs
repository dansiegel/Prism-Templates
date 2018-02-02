using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MonoDevelop.Ide.Templates;
using MonoDevelop.Projects;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class QuickStartTemplateWizard : TemplateWizard
    {
        public override string Id => "Prism.QuickStart.ProjectTemplateWizard";

        public override WizardPage GetPage(int pageNumber)
        {
            switch (pageNumber)
            {
                case 1: return new PrismAppInfoPage(this, Parameters["TemplateName"].Contains("QuickStart"));
                case 2: return new AppCenterPage(this);
                default: return new AuthenticationPage(this);
            }
        }

        public override int TotalPages => Parameters["TemplateName"].Contains("QuickStart") ? 3 : 1;

        public override void ItemsCreated(IEnumerable<IWorkspaceFileObject> items)
        {
            base.ItemsCreated(items);
            var sln = items.FirstOrDefault() as Solution;
            if (sln == null) return;

            var iOSProjectCreated = bool.Parse(Parameters["IncludeiOS"]);
            if (iOSProjectCreated)
            {
                var iOSProject = sln.Items.FirstOrDefault(p => p.FileName.FileName.Contains("iOS"));
                sln.StartupItem = iOSProject;

                FixDotNetTemplateEngineBug(iOSProject);
            }
        }

        private void FixDotNetTemplateEngineBug(IWorkspaceFileObject item)
        {
            foreach (var file in Directory.GetFiles(item.BaseDirectory, "*%40*.png", SearchOption.AllDirectories))
            {
                var fixedName = Regex.Replace(file, @"%40", "@");
                File.Move(file, fixedName);
            }
        }
    }
}
