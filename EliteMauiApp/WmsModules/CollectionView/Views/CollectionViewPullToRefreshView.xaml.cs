using Elite.LMS.Maui.WmsModules.Drawer.Data;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class CollectionViewPullToRefreshView : Wms.WmsPage {
        public CollectionViewPullToRefreshView() {
            InitializeComponent();
            BindingContext = new PullToRefreshViewModel(new MailMessagesRepository());
        }
    }
}
