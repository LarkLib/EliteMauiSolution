using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.Views {
    public partial class CustomerOrdersView : Wms.WmsPage {
        readonly Customer customer;

        public CustomerOrdersView() {
            InitializeComponent();

        }

        public CustomerOrdersView(Customer customer) : this() {
            this.customer = customer;
            this.BindingContext = customer;
        }
    }
}
