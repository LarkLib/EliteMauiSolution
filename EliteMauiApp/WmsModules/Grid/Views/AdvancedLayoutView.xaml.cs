using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.Views {
    public partial class AdvancedLayoutView : BaseGridContentPage {
        public AdvancedLayoutView() {
            InitializeComponent();
        }

        protected override object LoadData() {
            return new EmployeesRepository();
        }
    }
}
