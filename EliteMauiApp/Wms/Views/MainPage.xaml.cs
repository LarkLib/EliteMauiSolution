using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Elite.LMS.Maui.Data;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.ViewModels;
using DevExpress.Maui.Core;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Views;

public partial class MainPage : WmsPage
{
    public ICommand NavigationCommand { get; }
    private bool inNavigation;
    private ThemesPage themesPage;

    public MainPage()
    {
        InitializeComponent();
        NavigationCommand = new Command<Models.WmsItem>(async (obj) => await NavigateToDetailsPageAsync(obj.Module));
        themesPage = new ThemesPage();
    }

    async Task NavigateToDetailsPageAsync(Type wmsGroup)
    {
        if (inNavigation)
            return;

        inNavigation = true;
        IWmsData data = (IWmsData)Activator.CreateInstance(wmsGroup);
        Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "WmsData", data }
        };
        ControlPage detailsPage = new ControlPage();
        (detailsPage.BindingContext as ControlViewModel).ApplyQueryAttributes(parameters);
        await NavigationService.NavigateToPage(detailsPage, data.Title);
        inNavigation = false;
    }

    public async void WmsItemTappedControlShortcut(object sender, EventArgs e)
    {
        if (inNavigation)
            return;

        inNavigation = true;
        if (sender is DXButton dxButton)
        {
            var wmsItem = (WmsItem)dxButton.BindingContext;
            await NavigationService.NavigateToWms(wmsItem);
        }
        inNavigation = false;
    }

    private async void Theme_Tapped(object sender, EventArgs e)
    {
        await NavigationService.NavigateToPage(themesPage);
    }
    private void CloseApplication(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private void DXButton_Clicked(object sender, EventArgs e)
    {
        LoginUser.Instance.UserCode = "009";
    }
}