using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.CollectionView.Views;
using Elite.LMS.Maui.Models;

namespace Elite.LMS.Maui.Data {
    public class CollectionViewData : IWmsData {
        public CollectionViewData() {
            WmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "First Look",
                    Description = "An introduction to our Collection View control. Shows basic control features, such as vertical orientation, grouping, and item templates.",
                    Module = typeof(ContactsView),
                    Icon = "collectionview_firstlook"
                },
                new WmsItem() {
                    Title = "Filtering UI",
                    Description = "Shows built-in filtering UI available in our Collection View control. Users can apply filter conditions to individual columns or to the entire control (construct a filter criteria of any complexity).",
                    Module = typeof(FilteringUIView),
                    Icon = "collectionview_filteringui",
                },
                new WmsItem() {
                    Title = "CRUD Operations",
                    Description = "This wms implements a contact list. The list’s bound data source contains a few pre-defined contacts. You can edit existing records and add new people to the list. In both cases, you use the same contact edit form. Enter invalid values into editors on the form to see how our editors validate user input.",
                    Module = typeof(ContactsCRUDView),
                    Icon = "collectionview_contactscrud",
                },
                new WmsItem() {
                    Title = "Swipe Actions",
                    Description = "The swipe gesture allows your users to access row-related commands. Users can swipe a row to the left or right.",
                    Module = typeof(CollectionViewDefaultSwipes),
                    Icon = "collectionview_swipeactions"
                }
            };
        }

        public List<WmsItem> WmsItems { get; }
        public string Title => TitleData.WmsBaseInfoTitle;
    }
}
