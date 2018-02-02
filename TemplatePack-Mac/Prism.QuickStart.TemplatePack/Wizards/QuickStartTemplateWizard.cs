using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MonoDevelop.Ide.Templates;
using MonoDevelop.Projects;
using Prism.QuickStart.TemplatePack.Helpers;

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

        private bool IsQuickStart => Parameters["TemplateName"].Contains("QuickStart");

        public override int TotalPages => IsQuickStart ? 3 : 1;

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

            var appBase = Parameters["AppId"].Split('.').ToList();
            if (appBase.Count() > 2)
            {
                appBase.Remove(appBase.Last());
                UserSettings.Current.AppIdBase = string.Join(".", appBase);
            }
            UserSettings.Current.CreateEmptyQuickStart = bool.Parse(Parameters["Empty"]);
            UserSettings.Current.DIContainer = Parameters["Container"];
            UserSettings.Current.MinDroidSDK = int.Parse(Parameters["MinimumAndroidTarget"]);
            UserSettings.Current.UseMvvmHelpersLibrary = bool.Parse(Parameters["UseMvvmHelpers"]);
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
