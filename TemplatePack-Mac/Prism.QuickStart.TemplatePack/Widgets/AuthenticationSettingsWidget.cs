using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Ide.Templates;
using Xwt;
using Xwt.Drawing;

namespace Prism.QuickStart.TemplatePack.Widgets
{
    public class AuthenticationSettingsWidget : VBox
    {
        ComboBox authenticationSourceList;
        TextEntry clientIdEntry;
        TextEntry susiPolicyEntry;
        TextEntry editProfilePolicyEntry;
        TextEntry resetPasswordPolicyEntry;

        TemplateWizard _wizard;

        public AuthenticationSettingsWidget(TemplateWizard wizard)
        {
            _wizard = wizard;
            PackStart(new VBox { HeightRequest = 100 });

            var mainRow = new HBox();
            mainRow.PackStart(GetSideSpacer());
            var mainColumn = new VBox();

            authenticationSourceList = new ComboBox()
            {
                TooltipText = "Select an Authentication Provider"
            };
            authenticationSourceList.Items.Add("None");
            authenticationSourceList.Items.Add("Azure Active Directory");
            authenticationSourceList.Items.Add("Azure Active Directory B2C");
            authenticationSourceList.Items.Add("Custom");
            authenticationSourceList.SelectedItem = "None";
            authenticationSourceList.SelectionChanged += OnAuthenticationSourceChanged;

            var authSourceRow = new HBox();
            authSourceRow.PackStart(new Label { Text = "Authentication Source:" });
            authSourceRow.PackStart(authenticationSourceList);
            mainColumn.PackStart(authSourceRow);
            mainColumn.PackStart(new HSeparator());

            var clientIdRow = new HBox();
            clientIdRow.PackStart(new Label { Text = "Client Id:", MinWidth = 100 });
            clientIdEntry = new TextEntry
            {
                MinWidth = 250,
                Sensitive = false,
                TooltipText = "The Application Client Id"
            };
            clientIdEntry.TextInput += OnClientIdChanged;
            clientIdRow.PackStart(clientIdEntry);

            mainColumn.PackStart(clientIdRow);
            mainColumn.PackStart(new HSeparator());

            var table = new Table();
            susiPolicyEntry = new TextEntry
            {
                MinWidth = 250,
                Sensitive = false,
                TooltipText = "The Sign Up / Sign In Policy Name for Azure Active Directory B2C"
            };
            susiPolicyEntry.TextInput += OnSUSIPolicyChanged;
            editProfilePolicyEntry = new TextEntry
            {
                MinWidth = 250,
                Sensitive = false,
                TooltipText = "The Edit Profile Policy Name for Azure Active Directory B2C"
            };
            editProfilePolicyEntry.TextInput += OnEditProfilePolicyChanged;
            resetPasswordPolicyEntry = new TextEntry
            {
                MinWidth = 250,
                Sensitive = false,
                TooltipText = "The Password Reset Policy Name for Azure Active Directory B2C"
            };
            resetPasswordPolicyEntry.TextInput += OnResetPasswordPolicyChanged;

            table.Add(new Label { Text = "Azure Active Directory Options:" }, 0, 0, colspan: 2);

            table.Add(new Label { Text = "SUSI Policy:" }, 0, 1);
            table.Add(susiPolicyEntry, 1, 1);

            table.Add(new Label { Text = "Profile Edit Policy:" }, 0, 2);
            table.Add(editProfilePolicyEntry, 1, 2);

            table.Add(new Label { Text = "Password Reset Policy:" }, 0, 3);
            table.Add(resetPasswordPolicyEntry, 1, 3);

            mainColumn.PackStart(table);
            mainRow.PackStart(mainColumn);


            var assembly = GetType().Assembly;
            var socialImg = Image.FromStream(assembly.GetManifestResourceStream("socialmedia.png"));
            var socialImgView = new ImageView(socialImg)
            {
                MarginRight = 60,
                VerticalPlacement = WidgetPlacement.Center,
                HorizontalPlacement = WidgetPlacement.Center
            };

            mainRow.PackEnd(socialImgView);
            PackStart(mainRow);
        }

        private void OnResetPasswordPolicyChanged(object sender, TextInputEventArgs e) =>
            SetParameter("PolicyResetPassword", resetPasswordPolicyEntry);

        private void OnEditProfilePolicyChanged(object sender, TextInputEventArgs e) =>
            SetParameter("PolicyEditProfile", editProfilePolicyEntry);

        private void OnSUSIPolicyChanged(object sender, TextInputEventArgs e) =>
            SetParameter("PolicySignUpSignIn", susiPolicyEntry);

        private void OnClientIdChanged(object sender, TextInputEventArgs e) =>
            SetParameter("AuthenticationClientId", clientIdEntry);

        private void OnAuthenticationSourceChanged(object sender, EventArgs e)
        {
            bool enableAAD = false;
            bool enableClientId = false;
            switch (authenticationSourceList.SelectedText)
            {
                case "None":
                    _wizard.Parameters["AuthenticationSource"] = "None";
                    break;
                case "Azure Active Directory":
                    enableAAD = enableClientId = true;
                    _wizard.Parameters["AuthenticationSource"] = "AAD";
                    break;
                case "Azure Active Directory B2C":
                    enableAAD = enableClientId = true;
                    _wizard.Parameters["AuthenticationSource"] = "AADB2C";
                    break;
                case "Custom":
                    enableClientId = true;
                    _wizard.Parameters["AuthenticationSource"] = "Custom";
                    break;
            }

            susiPolicyEntry.Sensitive = enableAAD;
            resetPasswordPolicyEntry.Sensitive = enableAAD;
            editProfilePolicyEntry.Sensitive = enableAAD;

            clientIdEntry.Sensitive = enableClientId;
        }

        Box GetSideSpacer() => new VBox { WidthRequest = 60 };

        void SetParameter(string key, TextEntry entry) =>
            _wizard.Parameters[key] = entry.Text;
    }
}
