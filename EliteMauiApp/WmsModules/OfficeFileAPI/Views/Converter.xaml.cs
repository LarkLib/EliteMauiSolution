using Elite.LMS.Maui.WmsModules.OfficeFileAPI.ViewModels;

namespace Elite.LMS.Maui.Views;

public partial class ConverterWms : Wms.WmsPage {
    public ConverterWms() {
        InitializeComponent();
        BindingContext = new ConverterViewModel();
    }
}

