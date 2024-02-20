using System.Collections.Generic;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class ControlsData : IWmsData {
        readonly List<WmsItem> wmsItems;

        public ControlsData() {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Toolbar Control",
                    Description = "Wmsnstrates how to use DevExpress Toolbar control to create compact and adaptive action bars. The toolbar in this wms includes regular toolbar buttons, dropdowns, and a color picker. The control organizes commands into pages.",
                    Module = typeof(ToolbarView),
                    Icon = "edit",
                    WmsItemStatus = WmsItemStatus.New,
                },
                new WmsItem() {
                    Title = "Radial Gauge",
                    Description = "Shows how to use DevExpress radial gauge and progress bar components to implement a dashboard panel. In this wms, the dashboard monitors appliances and sensors in a smart home. To align gauges on screen, we use DevExpress layout controls - DXStackLayout and DXDockLayout.",
                    Icon = "radialgauge",
                    Module = typeof(RadialGaugeView),
                    WmsItemStatus = WmsItemStatus.New,
                },
                new WmsItem() {
                    Title = "Loading View",
                    Description = "This wms emulates an app that obtains its data from a remote server. While a load operation is in progress, the app switches to a Loading View. This view displays a skeleton layout with UI element placeholders with shimmer effects.",
                    Icon = "shimmer",
                    Module = typeof(ShimmerView),
                },
                new WmsItem() {
                    Title = "Simple Button",
                    Description = "Illustrates how to customize a button.",
                    Module = typeof(SimpleButtonView),
                    Icon = "simplebutton",
                    ShowItemUnderline = false
                }
            };
            this.wmsItems[this.wmsItems.Count - 1].ShowItemUnderline = false;
        }
        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.WmsQueryTitle;
    }
}
