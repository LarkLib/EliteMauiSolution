using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.Scheduler;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views {
    public partial class ReceptionDesk : Wms.WmsPage {
        bool inNavigation;

        public ReceptionDesk() {
            InitializeComponent();
            BindingContext = new ReceptionDeskWmsViewModel();
        }

        async void WorkWeekView_OnTap(object sender, SchedulerGestureEventArgs e) {
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
