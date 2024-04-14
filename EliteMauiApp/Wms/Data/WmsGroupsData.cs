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
                new WmsItem() {
                    Title = TitleData.WmsWarehouseTitle,
                    Description = "入库管理",
                    Module = typeof(WmsWarehouseData)
                },
            };
        }
        public static List<WmsItem> WmsItems => wmsItems;
    }
}