using DevExpress.Maui.Core;
using DevExpress.Maui.Editors;
using Elite.LMS.Maui.ViewModels;
using Elite.LMS.Maui.Services;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.WmsModules.CollectionView.Views {
    public partial class FilteringUIView : Wms.WmsPage {
        EnumToDescriptionConverter EnumToDescriptionConverter { get; } = new EnumToDescriptionConverter();

        public FilteringUIView() {
            InitializeComponent();
            BindingContext = new FilteringUIViewModel();
        }

        void OnCustomDisplayText(object sender, FilterElementCustomDisplayTextEventArgs e) {
            e.DisplayText = EnumToDescriptionConverter.Convert(e.Value).ToString();
        }
        void OnFilteringUIFormShowing(object sender, FilteringUIFormShowingEventArgs e) {
            if (e.Form is not ContentPage page)
                return;
            NavigationService.SetWmsPageTitleView(page, "Filters");
        }
    }
}
