using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.Scheduler;

namespace Elite.LMS.Maui.Views {
    public partial class MonthViewWms : Wms.WmsPage {
        readonly EmployeeCalendarViewModel viewModel = new EmployeeCalendarViewModel();
        bool inNavigation;

        public MonthViewWms() {
            InitializeComponent();
            BindingContext = viewModel;
        }

        async void MonthView_OnTap(object sender, SchedulerGestureEventArgs e) {
            if (inNavigation || e.IntervalInfo == null)
                return;
            viewModel.Start = e.IntervalInfo.Start;
            inNavigation = true;
            await NavigationService.NavigateToPage(new DayViewModule(viewModel, this.monthView.DataStorage));
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            inNavigation = false;
        }
    }
}
