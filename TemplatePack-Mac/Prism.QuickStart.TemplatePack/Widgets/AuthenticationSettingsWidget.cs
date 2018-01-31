using System;
using System.Collections.Generic;
using System.Linq;
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

        public AuthenticationSettingsWidget()
        {
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

            var authSourceRow = new HBox();
            authSourceRow.PackStart(new Label { Text = "Authentication Source:" });
            authSourceRow.PackStart(authenticationSourceList);
            mainColumn.PackStart(authSourceRow);
            mainColumn.PackStart(new HSeparator());

            var clientIdRow = new HBox();
            clientIdRow.PackStart(new Label { Text = "Client Id:" });
            clientIdEntry = new TextEntry
            {
                TooltipText = "The Application Client Id"
            };
            clientIdRow.PackStart(clientIdEntry);

            mainColumn.PackStart(clientIdRow);
            mainColumn.PackStart(new HSeparator());

            var table = new Table();
            susiPolicyEntry = new TextEntry
            {
                TooltipText = "The Sign Up / Sign In Policy Name for Azure Active Directory B2C"
            };
            editProfilePolicyEntry = new TextEntry
            {
                TooltipText = "The Edit Profile Policy Name for Azure Active Directory B2C"
            };
            resetPasswordPolicyEntry = new TextEntry
            {
                TooltipText = "The Password Reset Policy Name for Azure Active Directory B2C"
            };

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

        Box GetSideSpacer() => new VBox { WidthRequest = 60 };
    }
}
