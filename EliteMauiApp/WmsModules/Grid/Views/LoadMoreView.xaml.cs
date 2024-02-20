using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.Views {
    public partial class LoadMoreView : BaseGridContentPage {
        public LoadMoreView() {
            InitializeComponent();
        }

        protected override object LoadData() {
            return new OutlookDataRepository();
        }
    }
}
