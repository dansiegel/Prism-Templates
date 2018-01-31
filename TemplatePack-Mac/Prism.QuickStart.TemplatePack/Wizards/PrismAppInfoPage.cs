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

        //public string AppId
        //{
        //    get => _wizard.Parameters["AppId"];
        //    set => _wizard.Parameters["AppId"] = value;
        //}



        protected override object CreateNativeWidget<T>()
        {
            var view = new Widgets.BasicAppInfoWidget(_wizard.Parameters, _quickStart);
            return Toolkit.CurrentEngine.GetNativeWidget(view);
        }
    }
}
