using System;
using Elite.LMS.Maui.WmsModules.Grid.Data;
using Elite.LMS.Maui.WmsModules.Grid.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class RealTimeDataView : BaseGridContentPage {
        MainGridViewModel ViewModel { get; set; }
        public RealTimeDataView() {
            InitializeComponent();
            this.Disappearing += Handle_Disappearing;
        }
        protected override object LoadData() {
            ViewModel = new MainGridViewModel(new GridOrdersRepository());
            ViewModel.ForceSimulateMarketWorker();
            ViewModel.StartMarketSimulation();
            return ViewModel;
        }

        void Handle_Disappearing(object sender, EventArgs e) {
            ViewModel?.StopMarketSimulation();
        }
    }
}
