//using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.ViewModels;
using Elite.LMS.Maui.Wms.Views;
using Elite.LMS.Maui.WmsModules.Models;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views;

public partial class MaterialGroupBoxView : WmsPage, IBarcodeReceiver
{
    public MaterialGroupBoxView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var warehouseId = 2;// (Shell.GetTitleView(this) as Elite.LMS.Maui.Wms.TitleView).Title.Contains("µ¼Ïß") ? 1 : 2;
        var viewModel = new MaterialGroupBoxViewModel(warehouseId);
        BindingContext = viewModel;
    }
    // In your MainPage
#if ANDROID
    public void OnBarcodeReceive(string barcode)
    {
        (BindingContext as MaterialGroupBoxViewModel).MaterialBarcode = barcode;
    }
#endif
}
