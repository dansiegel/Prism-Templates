using System;
using System.Collections.Generic;
using MonoDevelop.Ide.Templates;
using Xwt;

namespace Prism.QuickStart.TemplatePack.Wizards
{
    public class PrismAppInfoPage : WizardPage
    {
        TemplateWizard _wizard { get; }
        bool _quickStart { get; }

        public PrismAppInfoPage(TemplateWizard wizard, bool quickStart = false)
        {
            _wizard = wizard;
            _quickStart = quickStart;
        }

        public override string Title => _quickStart ?
                            "QuickStart Prism Application Settings" :
                            "Prism Application Settings";
        //{
        //    get
        //    {
        //        string templateName = wizard.Parameters["TemplateName"];
        //        return $"{templateName}: Basic Info";
        //    }
        //}

        protected override object CreateNativeWidget<T>()
        {
            var view = new Widgets.BasicAppInfoWidget(_quickStart);
            return Toolkit.CurrentEngine.GetNativeWidget(view);
        }
    }
}
