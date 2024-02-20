using System;
using Elite.LMS.Maui.WmsModules.Grid.Data;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views {
    public partial class FirstLookView : BaseGridContentPage {
        public FirstLookView() {
            InitializeComponent();

#if PaidWmsModules
            ToolbarItems.Add(new ToolbarItem() {
                Text = "Export",
                IconImageSource = "export_button",
                Command = new Command(() => ExportButton_Clicked(null, null))
            });
            // TODO: Remove this line after Skia bug is fixed
            grid.Columns[0].AllowExport = false;
#endif
        }
        protected override object LoadData() {
            return new EmployeesRepository();
        }

        public void ExportButton_Clicked(object sender, EventArgs e) {
            this.bottomSheet.Show();
        }
    }
}