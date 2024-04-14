using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Wms.Views;
using Elite.LMS.Maui.WmsModules.Models;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views;

public partial class InitialInboundView : WmsPage, IBarcodeReceiver
{
    public InitialInboundView()
    {
        InitializeComponent();
    }
    // In your MainPage
#if ANDROID
    public void OnBarcodeReceive(string barcode)
    {
        this.txtMaterialCode.Text = barcode;
    }
#endif
}