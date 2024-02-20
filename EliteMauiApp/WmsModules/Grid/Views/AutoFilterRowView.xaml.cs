using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.Views {
    public partial class AutoFilterRowView : BaseGridContentPage {
        public AutoFilterRowView() {
            InitializeComponent();
        }

        protected override object LoadData() {
            return new OutlookDataRepository(300);
        }
    }
}
