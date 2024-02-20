using System.Collections.Generic;
using Elite.LMS.Maui.WmsModules.Popup.Views;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class DataFormData : IWmsData {
        readonly List<WmsItem> wmsItems;

        public DataFormData() {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Action Sheet",
                    Description = "A popup dialog is a floating window that appears on top of the current view and prevents access to it.",
                    Module = typeof(PopupDialogView),
                    Icon = "popupdialog"
                },
                new WmsItem() {
                    Title = "Context Menu",
                    Description = "Shows how to implement a context menu that appears when a user taps a button.",
                    Module = typeof(ContactsDropdownView),
                    Icon = "popupdropdown"
                },
                new WmsItem() {
                    Title = "Delivery Form",
                    Description = "A delivery form with a filled box style.",
                    Module = typeof(DeliveryFormView),
                    Icon = "deliveryform"
                },
                new WmsItem() {
                    Title = "Account Form",
                    Description = "An account form with an outlined box style.",
                    Module = typeof(DataFormAccountFormView),
                    Icon = "accountform"
                }
                ,
                new WmsItem() {
                    Title = "Employee Form",
                    Description = "An employee form with an outlined box style.",
                    Module = typeof(EmployeeFormView),
                    Icon = "employeeform",
                    ShowItemUnderline = false
                }
            };
        }

        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.DataFormDataTitle;
    }
}
