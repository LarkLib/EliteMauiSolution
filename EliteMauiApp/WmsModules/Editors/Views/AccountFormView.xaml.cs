
using System;
using Elite.LMS.Maui.WmsModules.Editors.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class AccountFormView : Wms.WmsPage {
        readonly AccountFormViewModel viewModel;

        public AccountFormView() {
            InitializeComponent();
            viewModel = new AccountFormViewModel();
            BindingContext = viewModel;
        }

        void OnSubmitClicked(Object sender, EventArgs e) {
            if (viewModel.Validate())
                DisplayAlert("Success", "Your account has been created successfully", "OK");
        }
    }
}
