using Elite.LMS.Maui.WmsModules.Editors.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class CheckEditView : Wms.WmsPage {
        public CheckEditView() {
            InitializeComponent();
            BindingContext = new CheckEditViewModel(this.edit);
        }
    }
}