﻿using System.Collections.Generic;
using Android.App;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;
using Elite.LMS.Maui.Wms.Views;

namespace Elite.LMS.Maui.Data
{
    public class EmptyData : IWmsData
    {
        readonly List<WmsItem> wmsItems;

        public EmptyData()
        {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "物料入库",
                    Description = "执行物料配盘入库任务",
                    Module = typeof(InboundView),
                    Icon = "grouping",
                    ShowItemUnderline = false,
                    WmsItemStatus = WmsItemStatus.New
                }
            };
        }
        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.WmsInboundTitle;
    }
}
