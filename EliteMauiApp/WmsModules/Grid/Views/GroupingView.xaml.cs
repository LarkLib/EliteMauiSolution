using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.Views {
    public partial class GroupingView : BaseGridContentPage {
        public GroupingView() {
            InitializeComponent();
        }
        protected override object LoadData() {
            return new InvoicesRepository();
        }
    }
}
