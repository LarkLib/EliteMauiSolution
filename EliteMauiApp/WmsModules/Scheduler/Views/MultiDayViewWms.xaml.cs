using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.Scheduler;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views {
    public partial class MultiDayViewWms : Wms.WmsPage {
        bool inNavigation;

        public MultiDayViewWms() {
            InitializeComponent();
            BindingContext = new EmployeeCalendarViewModel();
        }

        async void DayView_OnTap(object sender, SchedulerGestureEventArgs e) {
            if (inNavigation)
                return;
            Page appointmentPage = storage.CreateAppointmentPageOnTap(e, true);
            if (appointmentPage != null) {
                inNavigation = true;
                await NavigationService.NavigateToPage(appointmentPage);
            }
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            inNavigation = false;
        }
    }
}
