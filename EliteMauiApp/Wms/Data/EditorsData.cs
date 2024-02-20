using System;
using System.Collections.Generic;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class EditorsData : IWmsData {
        readonly List<WmsItem> wmsItems;

        public EditorsData() {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "First Look",
                    Description="You can use our editors to create a data form. This registration form allows a user to enter a login, password, date, and notes.",
                    Module = typeof(AccountFormView),
                    Icon = "editors"
                },
#if PaidWmsModules
                new WmsItem() {
                    Title = "Html Edit",
                    Description="Wmsnstrates how to use DevExpress HTML Edit control to replicate email client UI. The editor allows you to modify and format text, insert images, and add hyperlinks.",
                    Module = typeof(HtmlEditView),
                    Icon = "htmledit",
                    WmsItemStatus = WmsItemStatus.New,
                },
#endif
                new WmsItem() {
                    Title = "Image Edit",
                    Description="Wmsnstrates an avatar customization page created with the help of our image editor. The editor allows you to crop, flip, and rotate images. You can save the result in PNG and JPEG formats.",
                    Module = typeof(ImageEditPickerView),
                    Icon="imageedit",
                    WmsItemStatus = WmsItemStatus.New
                },
                new WmsItem() {
                    Title = "Settings Form",
                    Description="Wmsnstrates how to implement a settings form with the help of our editors. This form contains switches, text editors, and picker controls.",
                    Module = typeof(SettingsForm),
                    Icon = "settingsform",
                },
                new WmsItem() {
                    Title = "Deployment Form",
                    Description="Shows how to use our token editors to create an app deployment form. Token editors allow you to select multiple items from a list.",
                    Module = typeof(ApplicationDeploymentForm),
                    Icon="deploymentform",
                },
                new WmsItem() {
                    Title = "Combo Box",
                    Description = "Wmsnstrates the customization capabilities of our combo box.",
                    Module = typeof(ComboBoxEditView),
                    Icon = "combobox"
                },
                new WmsItem() {
                    Title = "Auto Complete",
                    ControlsPageTitle = "Text Editor with Autocomplete",
                    Description = "Shows how you can customize our text editor with the autocomplete feature.",
                    Module = typeof(AutoCompleteEditAsyncView),
                    Icon = "autocomplete"
                },
                new WmsItem() {
                    Title = "Phone Book",
                    PageTitle = "Phone Number Editor"+Environment.NewLine+"with Autocomplete",
                    ControlsPageTitle = "Phone Book with Autocomplete",
                    Description = "Shows how to use the text editor with autocomplete in a phone book.",
                    Module = typeof(AutoCompleteEditView),
                    Icon = "phonebook"
                },
                new WmsItem() {
                    Title = "Text Edit",
                    Description = "Wmsnstrates the customization capabilities of our text editor.",
                    Module = typeof(TextEditView),
                    Icon = "textedit"
                },
                new WmsItem() {
                    Title = "Numeric Edit",
                    Description = "Wmsnstrates the customization capabilities of our numeric editor.",
                    Module = typeof(NumericEditView),
                    Icon = "numericedit"
                },
                new WmsItem() {
                    Title = "Date Edit",
                    Description = "Shows how you can customize our date editor.",
                    Module = typeof(DateEditView),
                    Icon = "dateedit"
                },
                new WmsItem() {
                    Title = "Time Edit",
                    Description = "Wmsnstrates the customization capabilities of our time editor.",
                    Module = typeof(TimeEditView),
                    Icon = "timeedit"
                }
            };
        }

        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.EditorsDataTitle;
    }
}
