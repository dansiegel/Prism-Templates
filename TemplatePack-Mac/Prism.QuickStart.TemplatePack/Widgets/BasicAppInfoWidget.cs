using System;
using System.Collections.Generic;
using System.Linq;
using Xwt;

namespace Prism.QuickStart.TemplatePack.Widgets
{
    public class BasicAppInfoWidget : Xwt.Table
    {
        TextEntry projectNameTextBox;
        TextEntry appIdTextBox;
        CheckBox iOSCheckBox;
        CheckBox AndroidCheckBox;
        CheckBox UITestCheckBox;
        ComboBox containerList;
        CheckBox emptyProjectCheckBox;
        CheckBox barcodeServiceCheckBox;
        CheckBox localizationCheckBox;
        CheckBox mvvmHelpersCheckBox;
        CheckBox acrDialogsCheckBox;
        ComboBox minAndroidSDKList;
        ComboBox dataProviderList;

        public BasicAppInfoWidget(bool quickStart)
        {
            Include_iOS = true;
            Include_Android = true;
            SetupUIElements(quickStart);
            AttachEventHandlers(quickStart);
            AddElementsToTable(quickStart);
        }

        public string ProjectName { get; private set; }
        public string AppId { get; private set; }
        public bool Include_iOS { get; private set; }
        public bool Include_Android { get; private set; }
        public bool Include_UWP => false;
        public bool Include_UITests { get; private set; }
        public string DIContainer { get; private set; }

        private void SetupUIElements(bool quickStart)
        {
            projectNameTextBox = new TextEntry()
            {
                PlaceholderText = "Contoso.AwesomeApp",
                MinWidth = 120
            };

            appIdTextBox = new TextEntry()
            {
                PlaceholderText = "com.contoso.awesomeapp",
                TooltipText = "Sets the iOS 'CFBundleIdentifier' and Android manifest package name"
            };

            iOSCheckBox = new CheckBox()
            {
                Label = "iOS",
                TooltipText = "Include an iOS Project",
                State = CheckBoxState.On
            };

            AndroidCheckBox = new CheckBox()
            {
                Label = "Android",
                TooltipText = "Include an Android Project",
                State = CheckBoxState.On
            };

            UITestCheckBox = new CheckBox()
            {
                Label = "UI Tests",
                TooltipText = "Include a UI Test Project",
                State = CheckBoxState.Off
            };

            containerList = new ComboBox();
            containerList.Items.Add("Autofac");
            containerList.Items.Add("DryIoc");
            containerList.Items.Add("Unity");
            containerList.SelectedItem = DIContainer = "DryIoc";
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
                    State = CheckBoxState.Off
                };

                barcodeServiceCheckBox = new CheckBox()
                {
                    Label = "Include Barcode Scanner",
                    TooltipText = "Includes Barcode Scanning as a Service with ZXing.Net.Mobile",
                    State = CheckBoxState.Off
                };

                localizationCheckBox = new CheckBox()
                {
                    Label = "Localization Support",
                    TooltipText = "Includes Resx and XAML Translation extension for Localizing Strings",
                    State = CheckBoxState.On
                };

                mvvmHelpersCheckBox = new CheckBox()
                {
                    Label = "MVVM Helpers",
                    TooltipText = "Adds MVVM Helpers by James Montemagno",
                    State = CheckBoxState.On
                };

                acrDialogsCheckBox = new CheckBox()
                {
                    Label = "Acr.UserDialogs",
                    TooltipText = "Adds Acr.UserDialogs",
                    State = CheckBoxState.Off
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
            projectNameTextBox.Changed += OnProjectNameChanged;
            appIdTextBox.Changed += OnAppIdChanged;
            iOSCheckBox.Toggled += OnIncludeiOSChanged;
            AndroidCheckBox.Toggled += OnIncludeAndroidChanged;
            UITestCheckBox.Toggled += OnIncludeUITestChanged;
            containerList.SelectionChanged += OnDIContainerChanged;
            //minAndroidSDKList;

            if (quickStart)
            {
                //emptyProjectCheckBox
                //barcodeServiceCheckBox;
                //localizationCheckBox;
                //mvvmHelpersCheckBox;
                //acrDialogsCheckBox;
                //dataProviderList
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

        Box GetSpacerBox() => new HBox()
        {
            HeightRequest = 100,
            WidthRequest = 60
        };

        private void OnProjectNameChanged(object sender, EventArgs e) =>
            ProjectName = projectNameTextBox.Text.Trim();

        private void OnAppIdChanged(object sender, EventArgs e) =>
            AppId = appIdTextBox.Text.Trim().ToLower();

        private void OnIncludeiOSChanged(object sender, EventArgs e) =>
            Include_iOS = iOSCheckBox.State == CheckBoxState.On;

        private void OnIncludeAndroidChanged(object sender, EventArgs e) =>
            Include_Android = AndroidCheckBox.State == CheckBoxState.On;

        private void OnIncludeUITestChanged(object sender, EventArgs e) =>
            Include_UITests = UITestCheckBox.State == CheckBoxState.On;

        private void OnDIContainerChanged(object sender, EventArgs e) =>
            DIContainer = $"{containerList.SelectedItem}";

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (projectNameTextBox != null)
            {
                projectNameTextBox.Changed -= OnProjectNameChanged;
                projectNameTextBox.Dispose();
                projectNameTextBox = null;
            }

            if (appIdTextBox != null)
            {
                appIdTextBox.Changed -= OnAppIdChanged;
                appIdTextBox.Dispose();
                appIdTextBox = null;
            }

            if (iOSCheckBox != null)
            {
                iOSCheckBox.Toggled -= OnIncludeiOSChanged;
                iOSCheckBox.Dispose();
                iOSCheckBox = null;
            }

            if (AndroidCheckBox != null)
            {
                AndroidCheckBox.Toggled -= OnIncludeAndroidChanged;
                AndroidCheckBox.Dispose();
                AndroidCheckBox = null;
            }

            if (UITestCheckBox != null)
            {
                UITestCheckBox.Toggled -= OnIncludeUITestChanged;
                UITestCheckBox.Dispose();
                UITestCheckBox = null;
            }

            if (containerList != null)
            {
                containerList.SelectionChanged -= OnDIContainerChanged;
                containerList.Dispose();
                containerList = null;
            }
        }
    }
}
