using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.WmsModules.Services;
using Microsoft.Maui.Controls;
using System.Windows.Input;


namespace Elite.LMS.Maui.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public ICommand SubmitCommand { get; private set; }
    public ICommand CloseCommand { get; private set; }
    public string UserCode { get; set; } = "001";
    public string Password { get; set; } = "123456";

    public LoginViewModel()
    {

        SubmitCommand = new Command(() =>
        {
            try
            {
                var ret = WmsService.LoginUserValidation(new()
                {
                    LoginPwd = Password,
                    UserCode = UserCode,
                });
                if (!ret.Code.Equals("200")) throw new System.ApplicationException(ret.Msg);
                LoginUser.Instance.UserCode = ret.Data.UserCode;
                LoginUser.Instance.UserName = ret.Data.UserName;
                LoginUser.Instance.RoleName = ret.Data.RelationRoleName;
                LoginUser.Instance.UserId = ret.Data.ID.Value;
                LoginUser.Instance.RoleId = ret.Data.RelationRoleID.Value;
                LoginUser.Instance.LoginPwd = ret.Data.LoginPwd;

                //await NavigationService.NavigateToPage(new MainPage());
                Shell.SetTabBarIsVisible(Shell.Current.Items[0].Items[0], false);
                Shell.SetTabBarIsVisible(Shell.Current.Items[0].Items[1], true);
                Shell.Current.CurrentItem = Shell.Current.Items[0].Items[1];
            }
            catch (System.Exception ex)
            {
                utils.SendErrorMessage(ex.Message);
            }
        });

        CloseCommand = new Command(() => Application.Current.Quit());
    }
}
