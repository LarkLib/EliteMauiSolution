using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class LargeDataset : Wms.WmsPage {
        public LargeDataset() {
            InitializeComponent();
            BindingContext = new LargeDatasetViewModel(chart);
        }
    }
}
