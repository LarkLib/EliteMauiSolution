using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class CollectionViewHorizontalScrolling : Wms.WmsPage {
        public CollectionViewHorizontalScrolling() {
            InitializeComponent();
            BindingContext = new HorizontalScrollingModel();
        }
    }
}
