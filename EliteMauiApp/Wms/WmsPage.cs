using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Wms;

public class WmsPage : ContentPage {
    public WmsPage() {
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);
    }
}
