//using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.ViewModels;
using Elite.LMS.Maui.Wms.Views;
using Elite.LMS.Maui.WmsModules.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace Elite.LMS.Maui.Views;

public partial class MaterialGroupBoxExecuteView : WmsPage, IBarcodeReceiver
{
    private string LocalpLocalPlanCode { get; set; }
    public MaterialGroupBoxExecuteView(string localpLocalPlanCode)
    {
        InitializeComponent();
        LocalpLocalPlanCode = localpLocalPlanCode;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = new MaterialGroupBoxExecuteViewModel(LocalpLocalPlanCode);
        BindingContext = viewModel;
    }
    // In your MainPage
#if ANDROID
    public void OnBarcodeReceive(string barcode)
    {
        (BindingContext as MaterialGroupBoxExecuteViewModel).Barcodes.Add(barcode);
    }

    private void SimpleButton_Clicked(object sender, System.EventArgs e)
    {
        OnBarcodeReceive(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
    }

    private void SwipeItem_Tap(object sender, DevExpress.Maui.CollectionView.SwipeItemTapEventArgs e)
    {
    }
#endif
}
