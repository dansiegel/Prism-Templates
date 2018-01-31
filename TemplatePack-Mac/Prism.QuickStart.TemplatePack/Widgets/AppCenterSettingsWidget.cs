using System;
using System.Collections.Generic;
using System.Linq;
using Xwt;
using Xwt.Drawing;

namespace Prism.QuickStart.TemplatePack.Widgets
{
    public class AppCenterSettingsWidget : VBox
    {
        CheckBox useAppCenter;
        TextEntry iOSSecretEntry;
        TextEntry AndroidSecretEntry;

        public AppCenterSettingsWidget()
        {
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

            AndroidSecretEntry = new TextEntry();
            iOSSecretEntry = new TextEntry();

            optionsColumn.PackStart(useAppCenter);
            optionsColumn.PackStart(new HSeparator());

            //var row1 = new HBox();
            //row1.PackStart(GetSideSpacer());
            //row1.PackStart(useAppCenter);
            //PackStart(row1);
            //PackStart(new HSeparator() { MarginLeft = 60, MarginRight = 60 });

            var table = new Table();
            table.Add(new Label { Text = "Android Secret" }, 0, 0);
            table.Add(AndroidSecretEntry, 1, 0);
            table.Add(new Label { Text = "iOS Secret" }, 0, 1);
            table.Add(iOSSecretEntry, 1, 1);

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

        public VBox GetSideSpacer() => new VBox() { WidthRequest = 60 };
    }
}
