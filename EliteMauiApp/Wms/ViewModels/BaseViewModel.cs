using Elite.LMS.Maui.WmsModules;

namespace Elite.LMS.Maui.ViewModels;

public class BaseViewModel : NotificationObject
{
    public string Title { get; set; }
    protected ModelUtils utils = new ModelUtils();

}
