using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class FirstLookWms : Wms.WmsPage {
        public FirstLookWms() {
            InitializeComponent();
            BindingContext = new EmployeeCalendarViewModel();
        }
    }
}
