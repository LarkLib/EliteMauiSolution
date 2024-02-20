using Android.Views;
using CommunityToolkit.Maui.Views;
using Elite.LMS.Maui.WmsModules.Models;
using Microsoft.Maui.Controls;
using System;

namespace Elite.LMS.Maui.Wms.Views;

public partial class InboundView : ContentPage, IBarcodeReceiver
{
    public InboundView()
    {
        InitializeComponent();
    }
    async void OnButtonClicked(object sender, EventArgs args)
    {
        bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        await DisplayAlert("Alert", $"The result is {answer}", "OK");
        var popup = new Popup
        {
            Content = new VerticalStackLayout
            {
                Children =
                    {
                        new Label
                        {
                            Text = "This is a very important message!"
                        }
                    }
            }
        };
        this.ShowPopup(popup);
    }

#if ANDROID
    public void OnBarcodeReceive(string barcode)
    {
        this.txtPalletCode.Text = barcode;
    }
#endif
}