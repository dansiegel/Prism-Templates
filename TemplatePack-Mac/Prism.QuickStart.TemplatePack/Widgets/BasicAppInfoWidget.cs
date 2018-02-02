using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Ide.Templates;
using MonoDevelop.Projects;
using Xwt;
using static Prism.QuickStart.TemplatePack.Widgets.WidgetHelpers;

namespace Prism.QuickStart.TemplatePack.Widgets
{
    public class BasicAppInfoWidget : Xwt.Table
    {
        TextEntry projectNameTextBox;
        TextEntry appIdTextBox;
        CheckBox iOSCheckBox;
        CheckBox AndroidCheckBox;
        CheckBox UWPCheckBox;
        CheckBox macOSCheckBox;
        CheckBox UITestCheckBox;
        ComboBox containerList;
        CheckBox emptyProjectCheckBox;
        CheckBox barcodeServiceCheckBox;
        CheckBox localizationCheckBox;
        CheckBox mvvmHelpersCheckBox;
        CheckBox acrDialogsCheckBox;
        ComboBox minAndroidSDKList;
        ComboBox dataProviderList;

        TemplateWizard _wizard { get; }

        public BasicAppInfoWidget(TemplateWizard wizard, bool quickStart)
        {
            _wizard = wizard;
            //_wizard.Parameters["Empty"] = $"{true}";
            //_wizard.Parameters["IncludeUITest"] = $"{false}";

            SetupUIElements(quickStart);
            AttachEventHandlers(quickStart);
            AddElementsToTable(quickStart);
        }

        //public string ProjectName { get; private set; }
        //public string AppId { get; private set; }
        //public bool Include_iOS { get; private set; }
        //public bool Include_Android { get; private set; }
        //public bool Include_UWP => false;
        //public bool Include_UITests { get; private set; }
        //public string DIContainer { get; private set; }

        private void SetupUIElements(bool quickStart)
        {
            projectNameTextBox = new TextEntry()
            {
                PlaceholderText = "Awesome App",
                MinWidth = 250,
                TooltipText = "The Application Name that will be displayed on the User's Device. This is not the same as the Project/Solution Name."
            };

            appIdTextBox = new TextEntry()
            {
                PlaceholderText = "com.contoso.awesomeapp",
                TooltipText = "Sets the iOS 'CFBundleIdentifier' and Android manifest package name",
                MinWidth = 250
            };

            iOSCheckBox = new CheckBox()
            {
                Label = "iOS",
                TooltipText = "Create an iOS Project",
                State = GetCheckBoxState("IncludeiOS")
            };

            AndroidCheckBox = new CheckBox()
            {
                Label = "Android",
                TooltipText = "Create an Android Project",
                State = GetCheckBoxState("IncludeAndroid")
            };

            UWPCheckBox = new CheckBox
            {
                Label = "UWP",
                TooltipText = "Create a UWP Project. NOTE: This will not build or update in Visual Studio Mac",
                State = GetCheckBoxState("IncludeUWP")
            };

            macOSCheckBox = new CheckBox()
            {
                Label = "macOS",
                TooltipText = "Create an macOS Project",
                State = GetCheckBoxState("IncludeMacOS")
            };

            UITestCheckBox = new CheckBox()
            {
                Label = "UI Tests",
                TooltipText = "Include a UI Test Project",
                State = GetCheckBoxState("IncludeUITest")
            };

            containerList = new ComboBox();
            containerList.Items.Add("Autofac");
            containerList.Items.Add("DryIoc");
            containerList.Items.Add("Unity");
            containerList.SelectedItem = _wizard.Parameters["Container"];
            containerList.TooltipText = "Selects the DI Container to use for your Prism Application";

            minAndroidSDKList = new ComboBox();
            minAndroidSDKList.Items.Add(17);
            minAndroidSDKList.Items.Add(18);
            minAndroidSDKList.Items.Add(19);
            minAndroidSDKList.Items.Add(20);
            minAndroidSDKList.Items.Add(21);
            minAndroidSDKList.Items.Add(22);
            minAndroidSDKList.Items.Add(23);
            minAndroidSDKList.SelectedItem = 21;
            minAndroidSDKList.TooltipText = "Sets the Minimum Android SDK Version";

            if (quickStart)
            {
                emptyProjectCheckBox = new CheckBox()
                {
                    Label = "Empty Project",
                    TooltipText = "Creates the project without sample views",
                    State = GetCheckBoxState("Empty")
                };

                barcodeServiceCheckBox = new CheckBox()
                {
                    Label = "Include Barcode Scanner",
                    TooltipText = "Includes Barcode Scanning as a Service with ZXing.Net.Mobile",
                    State = GetCheckBoxState("IncludeBarcodeService")
                };

                localizationCheckBox = new CheckBox()
                {
                    Label = "Localization Support",
                    TooltipText = "Includes Resx and XAML Translation extension for Localizing Strings",
                    State = GetCheckBoxState("Localization")
                };

                mvvmHelpersCheckBox = new CheckBox()
                {
                    Label = "MVVM Helpers",
                    TooltipText = "Adds MVVM Helpers by James Montemagno",
                    State = GetCheckBoxState("UseMvvmHelpers")
                };

                acrDialogsCheckBox = new CheckBox()
                {
                    Label = "Acr.UserDialogs",
                    TooltipText = "Adds Acr.UserDialogs",
                    State = GetCheckBoxState("UseAcrDialogs")
                };

                dataProviderList = new ComboBox();
                dataProviderList.Items.Add("None");
                dataProviderList.Items.Add("Azure Mobile Client");
                dataProviderList.Items.Add("Realm");
                dataProviderList.SelectedItem = "None";
                dataProviderList.TooltipText = "Allows you to use Realm or the Azure Mobile Client for offline sync";
            }

        }

        private void AttachEventHandlers(bool quickStart)
        {
            //projectNameTextBox.Changed += OnProjectNameChanged;
            appIdTextBox.Changed += OnAppIdChanged;
            AndroidCheckBox.Clicked += OnIncludeAndroidChanged;
            iOSCheckBox.Clicked += OnIncludeiOSChanged;
            UWPCheckBox.Clicked += OnIncludeUWPChanged;
            macOSCheckBox.Clicked += OnIncludeMacOSChanged;
            UITestCheckBox.Clicked += OnIncludeUITestChanged;
            containerList.SelectionChanged += OnDIContainerChanged;
            minAndroidSDKList.SelectionChanged += OnMinAndroidSDKChanged;

            if (quickStart)
            {
                emptyProjectCheckBox.Clicked += OnEmptyProjectChanged;
                barcodeServiceCheckBox.Clicked += OnBarcodeServiceChanged;
                localizationCheckBox.Clicked += OnLocalizationChanged;
                mvvmHelpersCheckBox.Clicked += OnMvvmHelpersChanged;
                acrDialogsCheckBox.Clicked += OnAcrDialogsChanged;
                dataProviderList.SelectionChanged += OnDataProviderChanged;
            }
        }

        private void AddElementsToTable(bool quickStart)
        {
            this.ExpandHorizontal = true;
            this.ExpandVertical = true;
            this.HorizontalPlacement = WidgetPlacement.End;
            VerticalPlacement = WidgetPlacement.Fill;

            Add(GetSpacerBox(), 0, 0);
            Add(GetSpacerBox(), 8, 0);

            var assembly = GetType().Assembly;
            var prismImage = Xwt.Drawing.Image.FromStream(assembly.GetManifestResourceStream("prism-project-xamarin-forms.png"));

            Add(new ImageView(prismImage), 7, 2, rowspan: 6, vpos: WidgetPlacement.Center, marginLeft: 20);

            //Add(new Label { Text = "Project Name:" }, 1, 1);
            //Add(projectNameTextBox, 2, 1, colspan: 4);

            Add(new Label { Text = "App Id:" }, 1, 2);
            Add(appIdTextBox, 2, 2, colspan: 4);

            Add(new HSeparator(), 1, 3, colspan: 5);

            Add(new Label { Text = "Container:" }, 1, 4);
            Add(containerList, 2, 4, colspan: 2);

            Add(new HSeparator(), 1, 5, colspan: 5);
            var hBox = new HBox();
            hBox.PackStart(AndroidCheckBox);
            hBox.PackStart(iOSCheckBox);
            //hBox.PackStart(UWPCheckBox);
            //hBox.PackStart(macOSCheckBox);
            hBox.PackStart(UITestCheckBox);

            Add(new Label { Text = "Include Projects:" }, 1, 6);
            Add(hBox, 2, 6);

            Add(new Label { Text = "Min. Android SDK Version:" }, 1, 7);
            Add(minAndroidSDKList, 2, 7, colspan: 4);

            if (quickStart)
            {
                Add(new HSeparator(), 1, 8, colspan: 7);

                var optionsBox = new VBox();
                optionsBox.PackStart(new Label { Text = "Power Options" });

                var optionsRow = new HBox();

                var col1 = new VBox();
                col1.PackStart(emptyProjectCheckBox);
                col1.PackStart(acrDialogsCheckBox);
                optionsRow.PackStart(col1);

                var col2 = new VBox();
                col2.PackStart(localizationCheckBox);
                col2.PackStart(barcodeServiceCheckBox);
                optionsRow.PackStart(col2);

                var col3 = new VBox();
                col3.PackStart(mvvmHelpersCheckBox);
                optionsRow.PackStart(col3);

                optionsBox.PackStart(optionsRow);

                Add(optionsBox, 1, 9, colspan: 7);

                Add(new HSeparator(), 1, 10, colspan: 7);

                Add(new Label { Text = "Data Provider" }, 1, 11);
                Add(dataProviderList, 2, 11, colspan: 4);
            }
        }

        private void OnDataProviderChanged(object sender, EventArgs e) =>
            SetParameter("DataProvider", dataProviderList.SelectedItem);

        private void OnAcrDialogsChanged(object sender, EventArgs e) =>
            SetParameter("UseAcrDialogs", acrDialogsCheckBox);

        private void OnMvvmHelpersChanged(object sender, EventArgs e) =>
            SetParameter("UseMvvmHelpers", mvvmHelpersCheckBox);

        private void OnLocalizationChanged(object sender, EventArgs e) =>
            SetParameter("Localization", localizationCheckBox);

        private void OnBarcodeServiceChanged(object sender, EventArgs e) =>
            SetParameter("IncludeBarcodeService", barcodeServiceCheckBox);

        private void OnEmptyProjectChanged(object sender, EventArgs e) =>
            SetParameter("Empty", emptyProjectCheckBox);

        private void OnMinAndroidSDKChanged(object sender, EventArgs e) =>
            SetParameter("MinimumAndroidTarget", minAndroidSDKList.SelectedItem);

        //private void OnProjectNameChanged(object sender, EventArgs e) =>
        //ProjectName = projectNameTextBox.Text.Trim();

        private void OnAppIdChanged(object sender, EventArgs e) =>
            SetParameter("AppId", appIdTextBox.Text.Trim().ToLower());

        private void OnIncludeiOSChanged(object sender, EventArgs e) =>
            SetParameter("IncludeiOS", iOSCheckBox);

        private void OnIncludeAndroidChanged(object sender, EventArgs e)
        {
            SetParameter("IncludeAndroid", AndroidCheckBox);
            minAndroidSDKList.Sensitive = AndroidCheckBox.State == CheckBoxState.On;
        }

        private void OnIncludeUWPChanged(object sender, EventArgs e) =>
            SetParameter("IncludeUWP", UWPCheckBox);

        private void OnIncludeMacOSChanged(object sender, EventArgs e) =>
            SetParameter("IncludeMacOS", macOSCheckBox);

        private void OnIncludeUITestChanged(object sender, EventArgs e) =>
            SetParameter("IncludeUITest", UITestCheckBox);

        private void OnDIContainerChanged(object sender, EventArgs e) =>
            SetParameter("Container", containerList.SelectedItem);

        protected override void Dispose(bool disposing)
        {
            //DisposeElement(ref projectNameTextBox, OnProjectNameChanged)
            DisposeElement(ref appIdTextBox, OnAppIdChanged);
            DisposeElement(ref AndroidCheckBox, OnIncludeAndroidChanged);
            DisposeElement(ref iOSCheckBox, OnIncludeiOSChanged);
            DisposeElement(ref UWPCheckBox, OnIncludeUWPChanged);
            DisposeElement(ref macOSCheckBox, OnIncludeMacOSChanged);
            DisposeElement(ref UITestCheckBox, OnIncludeUITestChanged);
            DisposeElement(ref containerList, OnDIContainerChanged);
            DisposeElement(ref minAndroidSDKList, OnMinAndroidSDKChanged);
            DisposeElement(ref emptyProjectCheckBox, OnEmptyProjectChanged);
            DisposeElement(ref barcodeServiceCheckBox, OnBarcodeServiceChanged);
            DisposeElement(ref localizationCheckBox, OnLocalizationChanged);
            DisposeElement(ref mvvmHelpersCheckBox, OnMvvmHelpersChanged);
            DisposeElement(ref acrDialogsCheckBox, OnAcrDialogsChanged);
            DisposeElement(ref dataProviderList, OnDataProviderChanged);

            base.Dispose(disposing);
        }

        Box GetSpacerBox() => new HBox()
        {
            HeightRequest = 100,
            WidthRequest = 60
        };

        void SetParameter(string key, CheckBox cb) =>
            SetParameter(key, cb.State == CheckBoxState.On);

        void SetParameter(string key, object value) =>
            _wizard.Parameters[key] = $"{value}";

        bool GetBool(string key) =>
            bool.Parse(_wizard.Parameters[key]);

        CheckBoxState GetCheckBoxState(string key) =>
            bool.Parse(_wizard.Parameters[key]) ? CheckBoxState.On : CheckBoxState.Off;
    }
}
