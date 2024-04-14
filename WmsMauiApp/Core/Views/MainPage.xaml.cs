using Elite.LMS.Maui.Data;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Services;
using System.Windows.Input;

namespace Elite.LMS.Maui
{
    public partial class MainPage : ContentPage
    {
        public ICommand NavigationCommand { get; }
        private bool inNavigation;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void Theme_Tapped(object sender, EventArgs e)
        {
           // await NavigationService.NavigateToPage(themesPage);
        }
        public async void WmsItemTappedControlShortcut(object sender, EventArgs e)
        {
            if (inNavigation)
                return;

            inNavigation = true;
            if (sender is Button dxButton)
            {
                var wmsItem = (WmsItem)dxButton.BindingContext;
                await NavigationService.NavigateToWms(wmsItem);
            }
            inNavigation = false;
        }
    }

}
