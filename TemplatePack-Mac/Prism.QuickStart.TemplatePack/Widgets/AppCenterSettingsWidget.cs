﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Ide.Templates;
using Xwt;
using Xwt.Drawing;

namespace Prism.QuickStart.TemplatePack.Widgets
{
    public class AppCenterSettingsWidget : VBox
    {
        CheckBox useAppCenter;
        TextEntry iOSSecretEntry;
        TextEntry AndroidSecretEntry;
        TextEntry UWPSecretEntry;
        TextEntry MacOSSecretEntry;
        TemplateWizard _wizard;

        public AppCenterSettingsWidget(TemplateWizard wizard)
        {
            _wizard = wizard;
            PackStart(new VBox() { HeightRequest = 100 });

            var assembly = GetType().Assembly;

            var acLogo = Image.FromStream(assembly.GetManifestResourceStream("appcenter-100.png"));
            var logoView = new ImageView(acLogo)
            {
                MarginRight = 20,
                VerticalPlacement = WidgetPlacement.Start
            };

            var optionsRow = new HBox();
            optionsRow.PackStart(GetSideSpacer());
            optionsRow.PackStart(logoView);

            var optionsColumn = new VBox();
            useAppCenter = new CheckBox
            {
                Label = "Use App Center",
                TooltipText = "Adds AppCenter SDK to the project, including an implmentation of ILoggerFacade that logs to AppCenter Analytics",
                State = CheckBoxState.Off
            };
            useAppCenter.Clicked += OnUseAppCenterChanged;

            AndroidSecretEntry = new TextEntry()
            {
                MinWidth = 250,
                Sensitive = false
            };
            iOSSecretEntry = new TextEntry()
            {
                MinWidth = 250,
                Sensitive = false
            };
            UWPSecretEntry = new TextEntry()
            {
                MinWidth = 250,
                Sensitive = false
            };
            MacOSSecretEntry = new TextEntry()
            {
                MinWidth = 250,
                Sensitive = false
            };

            AndroidSecretEntry.TextInput += OnAndroidSecretChanged;
            iOSSecretEntry.TextInput += OniOSSecretChanged;
            UWPSecretEntry.TextInput += OnUWPSecretChanged;
            MacOSSecretEntry.TextInput += OnMacOSSecretChanged;

            optionsColumn.PackStart(useAppCenter);
            optionsColumn.PackStart(new HSeparator());

            //var row1 = new HBox();
            //row1.PackStart(GetSideSpacer());
            //row1.PackStart(useAppCenter);
            //PackStart(row1);
            //PackStart(new HSeparator() { MarginLeft = 60, MarginRight = 60 });

            var table = new Table();
            if (GetBool("IncludeAndroid"))
            {
                table.Add(new Label { Text = "Android Secret" }, 0, 0);
                table.Add(AndroidSecretEntry, 1, 0);
            }

            if (GetBool("IncludeiOS"))
            {
                table.Add(new Label { Text = "iOS Secret" }, 0, 1);
                table.Add(iOSSecretEntry, 1, 1);
            }

            if (GetBool("IncludeUWP"))
            {
                table.Add(new Label { Text = "UWP Secret" }, 0, 2);
                table.Add(UWPSecretEntry, 1, 2);
            }

            if (GetBool("IncludeMacOS"))
            {
                table.Add(new Label { Text = "macOS Secret" }, 0, 3);
                table.Add(MacOSSecretEntry, 1, 3);
            }

            optionsColumn.PackStart(table);
            optionsRow.PackStart(optionsColumn);

            //var secrets = new VBox();
            //secrets.PackStart(AndroidSecretEntry);
            //secrets.PackStart(iOSSecretEntry);

            //var row2 = new HBox();
            //row2.PackStart(GetSideSpacer());
            //row2.PackStart(table);
            //PackStart(row2);

            var overviewImg = Image.FromStream(assembly.GetManifestResourceStream("appcenter-overview.png"));
            optionsRow.PackEnd(new ImageView(overviewImg)
            {
                HorizontalPlacement = WidgetPlacement.Center,
                VerticalPlacement = WidgetPlacement.Center,
                MarginRight = 60,
                MarginLeft = 30
            });
            PackStart(optionsRow);

            //PackStart(new ImageView(overviewImg)
            //{
            //	HorizontalPlacement = WidgetPlacement.Center
            //});

        }

        private void OnUseAppCenterChanged(object sender, EventArgs e)
        {
            var enable = useAppCenter.State == CheckBoxState.On;
            _wizard.Parameters["UseAppCenter"] = $"{enable}";
            AndroidSecretEntry.Sensitive = iOSSecretEntry.Sensitive =
                UWPSecretEntry.Sensitive = MacOSSecretEntry.Sensitive = enable;
        }

        private void OnAndroidSecretChanged(object sender, TextInputEventArgs e) =>
            _wizard.Parameters["AppCenter-Android"] = AndroidSecretEntry.Text;

        private void OniOSSecretChanged(object sender, TextInputEventArgs e) =>
            _wizard.Parameters["AppCenter-iOS"] = iOSSecretEntry.Text;

        private void OnUWPSecretChanged(object sender, TextInputEventArgs e) =>
            _wizard.Parameters["AppCenter-UWP"] = UWPSecretEntry.Text;

        private void OnMacOSSecretChanged(object sender, TextInputEventArgs e) =>
            _wizard.Parameters["AppCenter-macOS"] = MacOSSecretEntry.Text;

        public VBox GetSideSpacer() => new VBox() { WidthRequest = 60 };

        bool GetBool(string key) => bool.Parse(_wizard.Parameters[key]);
    }
}
