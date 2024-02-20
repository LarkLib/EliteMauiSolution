using Elite.LMS.Maui.WmsModules.Drawer.Data;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class CollectionViewLoadMoreView : Wms.WmsPage {
        public CollectionViewLoadMoreView() {
            InitializeComponent();
            BindingContext = new LoadMoreViewModel(new MailMessagesRepository());
        }
    }
}
