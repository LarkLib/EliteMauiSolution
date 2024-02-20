using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.CollectionView.Data;

namespace Elite.LMS.Maui.ViewModels {
    public class HorizontalScrollingModel : NotificationObject {
        readonly HouseSalesRepository repository = new HouseSalesRepository();
        public IList<House> ItemSource => this.repository.Houses;

        public HorizontalScrollingModel() {
        }
    }
}
