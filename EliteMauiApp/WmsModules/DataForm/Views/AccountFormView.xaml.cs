using System;
using Elite.LMS.Maui.Wms;
using Elite.LMS.Maui.WmsModules.DataForm.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class DataFormAccountFormView : AdaptivePage {
        public DataFormAccountFormView() {
            InitializeComponent();
            BindingContext = new AccountFormViewModel();
            OrientationChanged += OnOrientationChanged;
        }

        void OnOrientationChanged(object sender, EventArgs e) {
            ((AccountFormViewModel)this.BindingContext).Rotate(dataForm, Orientation);
        }

        void SubmitOnClicked(object sender, EventArgs e) {
            if (dataForm.Validate()) {
                dataForm.Commit();
                DisplayAlert("Success", "Your account has been created successfully", "OK");
            }
        }
    }
}
