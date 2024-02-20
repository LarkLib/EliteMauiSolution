using Elite.LMS.Maui.WmsModules.Grid.Data;
using Elite.LMS.Maui.WmsModules.Grid.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class RowAutoHeightView : BaseGridContentPage {
        public RowAutoHeightView() {
            InitializeComponent();
        }

        protected override object LoadData() {
            return new MainGridViewModel(new GridOrdersRepository());
        }
    }
}
