using System;
using System.Threading.Tasks;
using Elite.LMS.Maui.Wms;
using Elite.LMS.Maui.Models;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.Services
{
    public static class NavigationService
    {
        public static void SetWmsPageTitleView(Page page, string titleText)
        {
            Shell.SetTitleView(page, new TitleView() { Title = titleText });
        }

        public static async Task NavigateToWms(WmsItem wmsItem)
        {
            Page wmsPage = (Page)Activator.CreateInstance(wmsItem.Module);

            await NavigateToPage(wmsPage, wmsItem);
        }

        public static async Task NavigateToPage(Page page, WmsItem wmsItem = null)
        {
            string titleText = (wmsItem == null) ? page.Title : wmsItem.Title;
            await NavigateToPage(page, titleText);
        }

        public static async Task NavigateToPage(Page page, string titleText)
        {
            if (Shell.GetTitleView(page) == null)
                SetWmsPageTitleView(page, titleText);

            await Shell.Current.Navigation.PushAsync(page);
        }
    }
}
