using System.Collections.Generic;
using Android.App;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.ViewModels;
using Elite.LMS.Maui.Views;
using Elite.LMS.Maui.Wms.Views;

namespace Elite.LMS.Maui.Data
{
    public class WmsWarehouseData : IWmsData
    {
        readonly List<WmsItem> wmsItems;

        public WmsWarehouseData()
        {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "初始入库",
                    Description = "执行初始入库任务",
                    Module = typeof(InitialInboundView),
                    Icon = "initial_inbound",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.Wms
                },
                new WmsItem() {
                    Title = "退料入库",
                    Description = "执行退料入库任务",
                    Module = typeof(MaterialReturnView),
                    Icon = "material_return",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.Wms
                },
                new WmsItem() {
                    Title = "手工领料(导线)",
                    Description = "执行导线手工领料任务",
                    Module = typeof(ManuallyIssueView),
                    Icon = "manually_issue_circle",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.Wms
                },
                new WmsItem() {
                    Title = "手工领料(端子)",
                    Description = "执行端子手工领料任务",
                    Module = typeof(ManuallyIssueView),
                    Icon = "manually_issue_rect",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.Wms
                },
                new WmsItem() {
                    Title = "原料组箱",
                    Description = "执行原料组箱任务",
                    Module = typeof(MaterialGroupBoxView),
                    Icon = "grouping",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.New
                },
                new WmsItem() {
                    Title = "盘点入库",
                    Description = "执行盘点入库任务",
                    Module = typeof(NewContent1),
                    Icon = "grouping",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.New
                },
                new WmsItem() {
                    Title = "空轴处理",
                    Description = "执行空轴处理任务",
                    Module = typeof(NewContent1),
                    Icon = "grouping",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.New
                },

            };
        }
        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.WmsWarehouseTitle;
    }
}
