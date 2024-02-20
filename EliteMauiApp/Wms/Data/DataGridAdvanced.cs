using System.Collections.Generic;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class DataGridAdvanced : IWmsData {
        readonly List<WmsItem> wmsItems;

        public DataGridAdvanced() {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Drag and Drop",
                    Description = "Users can drag a row to a new position. Touch and hold a row to start dragging it.",
                    Module = typeof(DragDropView),
                    Icon = "dragdrop",
                    WmsItemStatus = WmsItemStatus.New
                },
                new WmsItem() {
                    Title = "Advanced Layout",
                    Description = "The Data Grid control supports column layouts of any complexity. You can arrange data in your table so that it is easier for your customers to read it.",
                    Module = typeof(AdvancedLayoutView),
                    Icon = "advancedlayout",
                    WmsItemStatus = WmsItemStatus.Updated
                },
                new WmsItem() {
                    Title = "Custom Appearance",
                    Description = "You can customize the appearance of specific rows and cells based on their position, values, or any other logic.",
                    Module = typeof(CustomAppearanceView),
                    Icon = "gridcustomappearance"
                },
                new WmsItem() {
                    Title = "Row Auto Height",
                    Description = "The Data Grid control automatically adjusts row heights to fully display the content of cells. You can always set the height to a fixed value.",
                    Module = typeof(RowAutoHeightView),
                    Icon = "rowautoheight"
                }
            };
        }
        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.WmsOutboundTitle;
    }
}

