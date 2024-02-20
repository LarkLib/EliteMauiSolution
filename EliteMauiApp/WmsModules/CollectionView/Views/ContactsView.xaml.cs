using System.Threading.Tasks;
using Elite.LMS.Maui.WmsModules.TabView.Pages;
using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.CollectionView;

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class ContactsView : Wms.WmsPage {
        static PhoneContact GetContactInfo(object item) {
            if (item is CallInfo callInfo)
                return callInfo?.Contact;
            return null;
        }
        bool inNavigation;

        public ContactsView() {
            InitializeComponent();
            BindingContext = new ContactsViewModel();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            this.inNavigation = false;
        }

        public async void ItemSelected(object sender, CollectionViewGestureEventArgs args) {
            if (args.Item != null)
                await OpenDetailPage(GetContactInfo(args.Item));
        }

        Task OpenDetailPage(PhoneContact contact) {
            if (contact == null)
                return Task.CompletedTask;

            if (this.inNavigation)
                return Task.CompletedTask;

            this.inNavigation = true;
            return NavigationService.NavigateToPage(new ContactDetailPage(contact));
        }
    }
}
