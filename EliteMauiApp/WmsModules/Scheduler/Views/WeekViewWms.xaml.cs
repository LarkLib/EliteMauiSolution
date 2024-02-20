using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.Scheduler;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views {
    public partial class WeekViewWms : Wms.WmsPage {
        bool inNavigation;

        public WeekViewWms() {
            InitializeComponent();
            BindingContext = new EmployeeCalendarViewModel();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            this.inNavigation = false;
        }

        async void WeekView_OnTap(object sender, SchedulerGestureEventArgs e) {
            if (this.inNavigation)
                return;
            Page appointmentPage = this.storage.CreateAppointmentPageOnTap(e, true);
            if (appointmentPage != null) {
                this.inNavigation = true;
                await NavigationService.NavigateToPage(appointmentPage);
            }
        }
    }
}
