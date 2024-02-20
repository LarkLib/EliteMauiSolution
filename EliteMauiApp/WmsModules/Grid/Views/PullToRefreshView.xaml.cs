using Elite.LMS.Maui.WmsModules.Grid.Data;
using Elite.LMS.Maui.WmsModules.Grid.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class PullToRefreshView : BaseGridContentPage {
        public PullToRefreshView() {
            InitializeComponent();
        }
        protected override object LoadData() {
            MainGridViewModel viewModel = new MainGridViewModel(new GridOrdersRepository());
            viewModel.ForceSimulateMarketWorker();
            return viewModel;
        }
    }
}
