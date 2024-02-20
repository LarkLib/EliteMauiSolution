
using System;
using Elite.LMS.Maui.WmsModules.Editors.ViewModels;
using Elite.LMS.Maui.Services;

namespace Elite.LMS.Maui.Views {
    public partial class SettingsForm : Wms.WmsPage {
        public SettingsForm() {
            InitializeComponent();
        }

        async void OnBioTap(object sender, EventArgs e) {
            await NavigationService.NavigateToPage(new EditBioPage() { Settings = (SettingsFormViewModel)BindingContext });
        }
    }
}
