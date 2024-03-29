using System;
using System.Timers;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class Oscillator : Wms.WmsPage {
        readonly OscillatorChartsViewModel viewModel = new OscillatorChartsViewModel();
        readonly Timer timer = new Timer();
        bool isRunning;

        public Oscillator() {

            InitializeComponent();
            BindingContext = viewModel;

            timer.Interval = 20;
            timer.Elapsed += Timer_Tick;
            timer.AutoReset = false;
        }

        void Timer_Tick(object sender, EventArgs e) {
            Dispatcher.Dispatch(() => {
                viewModel.MoveToNextFrame();
                if (isRunning)
                    timer.Start();
            });
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            isRunning = false;
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            isRunning = true;
            timer.Start();
        }
    }
}
