using Elite.LMS.Maui.WmsModules.Grid.Data;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class CollectionViewRowAutoHeightView : Wms.WmsPage {
        public CollectionViewRowAutoHeightView() {
            InitializeComponent();
            BindingContext = new AutoHeightViewModel(new GridOrdersRepository());
        }
    }
}
