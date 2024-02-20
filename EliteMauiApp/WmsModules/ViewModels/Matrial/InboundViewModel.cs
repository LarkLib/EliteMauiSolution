using Android.Content;
using CommunityToolkit.Maui.Views;
using Elite.LMS.Maui.ViewModels;
using Elite.LMS.Maui.WmsModules.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Elite.LMS.Maui.ViewModels;

public class InboundViewModel : BaseViewModel
{
    public Monkey[] Monkeys => [new Monkey() { Name = "aaa", ImageUrl = "", Location = "Location aaa" }, new Monkey() { Name = "bbb", ImageUrl = "", Location = "Location bbb" }];
    public ICommand InboundCommand { get; private set; }
    public string MaterialCode { get; set; } = "CF-000151";
    public InboundViewModel()
    {
        InboundCommand = new Command(() =>
        {
            var ret = WmsService.GetMaterialByCodeAsync(MaterialCode);
            var count = ret.Msg;
        });
    }
}

public class Monkey
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Location { get; set; }
}
