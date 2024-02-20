using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Views {
    public partial class FinancialChart : Wms.WmsPage {
        public FinancialChart() {

            InitializeComponent();
            BindingContext = new FinancialChartViewModel();
        }
    }
}
