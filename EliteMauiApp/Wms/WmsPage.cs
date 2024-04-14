using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using Elite.LMS.Maui.Controls;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Wms.Views;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Elite.LMS.Maui.Views;

public class WmsPage : ContentPage
{
    public WmsPage()
    {
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);
        Shell.SetTabBarIsVisible(this, false);
    }
}
