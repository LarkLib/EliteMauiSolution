using DevExpress.Maui.DataForm;
using Elite.LMS.Maui.WmsModules.OfficeFileAPI.ViewModels;
using Microsoft.Maui.Controls;
using System.Linq;
using System;
using Elite.LMS.Maui.Wms;

namespace Elite.LMS.Maui.Views;

public partial class FillPDFEditFieldsPage : Wms.WmsPage {
    public string Document { get; set; }
    public FillPDFEditFieldsPage() {
        InitializeComponent();
        Shell.SetTitleView(this, new TitleView() { Title = Title });
    }
    private void SavePDF(System.Object sender, System.EventArgs e) {
        dataform.Commit();
        ((FillPDFEditPageViewModel)this.BindingContext).SaveEditsCommand.Execute(null);
    }
    private void dataform_ValidateProperty(object sender, DataFormPropertyValidationEventArgs e) {
        DataFormItem dataFormItem = ((DataFormView)sender).Items.FirstOrDefault(item => item.FieldName == e.PropertyName);
        if (dataFormItem != null && ((EditedItemModel)dataFormItem.BindingContext).IsRequired && (e.NewValue == null || (e.NewValue is string strValue && string.IsNullOrEmpty(strValue)))) {
            e.HasError = true;
        }
    }
}
public class FormItemTemplateSelector : DataTemplateSelector {
    public DataTemplate TextFormDataItemTemplate {
        get;
        set;
    }
    public DataTemplate DateFormDataItemTemplate {
        get;
        set;
    }
    public DataTemplate ComboBoxDataFormItemTemplate {
        get;
        set;
    }
    public DataTemplate MaskDataFormItemTemplate {
        get;
        set;
    }
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
        if (item == null)
            return null;
        Type type = item.GetType();
        if (type == typeof(EditedItemModel))
            return TextFormDataItemTemplate;
        else if (type == typeof(ComboBoxEditedItemModel))
            return ComboBoxDataFormItemTemplate;
        else if (type == typeof(DateEditedItemModel))
            return DateFormDataItemTemplate;
        else if (type == typeof(MaskEditedItemModel))
            return MaskDataFormItemTemplate;
        throw new NotSupportedException("Implement logic for your item type in FormItemTemplateSelector");
    }
}

