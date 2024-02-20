using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.Grid.Data;

namespace Elite.LMS.Maui.ViewModels {
    public class AutoHeightViewModel : NotificationObject {
        readonly OrdersRepository repository;

        public IList<Customer> ItemSource => this.repository.Customers;

        public AutoHeightViewModel(OrdersRepository repository) {
            this.repository = repository;
        }
    }
}
