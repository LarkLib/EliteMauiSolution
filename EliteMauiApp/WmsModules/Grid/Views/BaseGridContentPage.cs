using System.Threading.Tasks;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Elite.LMS.Maui.Views {
    public abstract class BaseGridContentPage : Wms.WmsPage {
        public BaseGridContentPage() {
            On<Microsoft.Maui.Controls.PlatformConfiguration.iOS>().SetUseSafeArea(false);
            BindData();
        }

        async void BindData() {
            BindingContext = await Task.Run(LoadData);
        }

        protected abstract object LoadData();
    }
}
