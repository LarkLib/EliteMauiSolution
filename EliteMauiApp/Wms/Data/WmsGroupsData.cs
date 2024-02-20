using System.Collections.Generic;
using Elite.LMS.Maui.Models;

namespace Elite.LMS.Maui.Data
{
    public class WmsGroupsData
    {
        static readonly List<WmsItem> wmsItems;

        static WmsGroupsData()
        {
            wmsItems = new List<WmsItem>() {
                //new WmsItem() {
                //    Title = TitleData.WmsBaseInfoTitle,
                //    Description = "基础信息查询",
                //    Module = typeof(EmptyData)
                //},
                //new WmsItem() {
                //    Title = TitleData.WmsQueryTitle,
                //    Description = "库存信息查询",
                //    Module = typeof(EmptyData)
                //},
                new WmsItem() {
                    Title = TitleData.WmsInboundTitle,
                    Description = "入库管理",
                    Module = typeof(EmptyData)
                },
                //new WmsItem() {
                //    Title = TitleData.WmsOutboundTitle,
                //    Description = "出库管理",
                //    Module = typeof(EmptyData)
                //}
            };
        }
        public static List<WmsItem> WmsItems => wmsItems;
    }
}