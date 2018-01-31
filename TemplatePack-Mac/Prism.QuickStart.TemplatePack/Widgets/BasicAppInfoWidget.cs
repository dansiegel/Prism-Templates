using System;
using System.Collections.Generic;
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

        public BasicAppInfoWidget()
        {
            Include_iOS = true;
            Include_Android = true;
            SetupUIElements();
            AttachEventHandlers();
        }

        public string ProjectName { get; private set; }
        public string AppId { get; private set; }
        public bool Include_iOS { get; private set; }
        public bool Include_Android { get; private set; }
        public bool Include_UWP => false;
        public bool Include_UITests { get; private set; }
        public string DIContainer { get; private set; }

        private void SetupUIElements()
        {
            projectNameTextBox = new TextEntry()
            {
                PlaceholderText = "Contoso.AwesomeApp"
            };

            appIdTextBox = new TextEntry()
            {
                PlaceholderText = "com.contoso.awesomeapp"
            };

            iOSCheckBox = new CheckBox()
            {
                Label = "iOS",
                State = CheckBoxState.On
            };

            AndroidCheckBox = new CheckBox()
            {
                Label = "Android",
                State = CheckBoxState.On
            };

            UITestCheckBox = new CheckBox()
            {
                Label = "UI Tests",
                State = CheckBoxState.Off
            };

            containerList = new ComboBox();
            containerList.Items.Add("Autofac");
            containerList.Items.Add("DryIoc");
            containerList.Items.Add("Unity");
            containerList.SelectedItem = DIContainer = "DryIoc";
        }

        private void AttachEventHandlers()
        {
            projectNameTextBox.Changed += OnProjectNameChanged;
            appIdTextBox.Changed += OnAppIdChanged;
            iOSCheckBox.Toggled += OnIncludeiOSChanged;
            AndroidCheckBox.Toggled += OnIncludeAndroidChanged;
            UITestCheckBox.Toggled += OnIncludeUITestChanged;
            containerList.SelectionChanged += OnDIContainerChanged;
        }

        private void AddElementsToTable()
        {
            Add(projectNameTextBox, 0, 0);
            Add(appIdTextBox, 0, 1);
            Add(containerList, 0, 2);
        }

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
                projectNameTextBox = null;
            }

            if (appIdTextBox != null)
            {
                appIdTextBox.Changed -= OnAppIdChanged;
                appIdTextBox = null;
            }

            if (iOSCheckBox != null)
            {
                iOSCheckBox.Toggled -= OnIncludeiOSChanged;
                iOSCheckBox = null;
            }

            if (AndroidCheckBox != null)
            {
                AndroidCheckBox.Toggled -= OnIncludeAndroidChanged;
                AndroidCheckBox = null;
            }

            if (UITestCheckBox != null)
            {
                UITestCheckBox.Toggled -= OnIncludeUITestChanged;
                UITestCheckBox = null;
            }

            if (containerList != null)
            {
                containerList.SelectionChanged -= OnDIContainerChanged;
                containerList = null;
            }
        }
    }
}
