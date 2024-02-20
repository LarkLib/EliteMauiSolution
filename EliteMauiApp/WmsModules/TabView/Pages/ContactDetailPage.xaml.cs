using Elite.LMS.Maui.WmsModules.TabView.ViewModels;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.WmsModules.TabView.Pages {
    public partial class ContactDetailPage : Wms.WmsPage {
        readonly ContactDetailPageViewModel viewModel;
        public ContactDetailPage(PhoneContact contactInfo) {
            this.viewModel = new ContactDetailPageViewModel(contactInfo);
            InitializeComponent();
            BindingContext = this.viewModel;
        }
    }
}
