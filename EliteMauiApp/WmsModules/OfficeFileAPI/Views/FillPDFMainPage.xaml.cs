using Elite.LMS.Maui.WmsModules.OfficeFileAPI.ViewModels;
using Microsoft.Maui.Controls;
namespace Elite.LMS.Maui.Views;

public partial class FillPDFMainPage : Wms.WmsPage {
    public FillPDFMainPage() {
        InitializeComponent();
    }

    private void NavigatedToPage(object sender, NavigatedToEventArgs args) {
        ((FillPDFMainPageViewModel)BindingContext).UpdatePreview();
    }
}

