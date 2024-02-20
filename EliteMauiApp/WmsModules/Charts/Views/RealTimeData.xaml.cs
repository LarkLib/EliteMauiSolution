using DevExpress.Maui.Core;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class RealTimeData : Wms.WmsPage {
        RealTimeDataViewModel viewModel;
        static readonly bool IsOniOSSimulator = ON.Simulator && ON.iOS;
        public RealTimeData() {
            InitializeComponent();
            BindingContext = viewModel = new RealTimeDataViewModel(chart);
        }
        protected override void OnDisappearing() {
            base.OnDisappearing();
            if (!IsOniOSSimulator) {
                viewModel.Stop();
            }
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            if (!IsOniOSSimulator) {
                viewModel.Start();
            } else {
                DisplayAlert("Accelerometer not found", "This wms is available only on the real device.", "Ok");
            }
        }
    }
}
