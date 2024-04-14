using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Elite.LMS.Maui.ViewModels;

public class MaterialGroupBoxExecuteViewModel : BaseViewModel
{
    public ICommand BoxingPlanCommand { get; private set; }
    public ICommand EndSwipeCommand { get; private set; }
    public string LocalpLocalPlanCode { get; set; }
    public ObservableCollection<string> Barcodes { get; private set; } = new ObservableCollection<string>() { "aaa", "bbb", "ccc" };

    public MaterialGroupBoxExecuteViewModel(string localpLocalPlanCode)
    {
        LocalpLocalPlanCode = localpLocalPlanCode;
        BoxingPlanCommand = new Command(async () =>
        {
            try
            {
                //await Task.Delay(0);
                var confirmed = await utils.SendConfirmMessage("请确认是否提信息！");
                if (confirmed != true) return;
                return;
                //var ret = WmsService.ExecuteManuallyIssuePlan(new PlanExecuteRequestBody
                //{
                //    LocalPlanCode = LocalpLocalPlanCode,
                //    UserID = LoginUser.Instance.UserId,
                //    UserCode = LoginUser.Instance.UserCode,
                //    UserName = LoginUser.Instance.UserName
                //});
                //if (!ret.Code.Equals("200")) throw new System.ApplicationException(ret.Msg);
            }
            catch (System.Exception ex)
            {

                utils.SendErrorMessage(ex.Message);
            }
        });
        EndSwipeCommand = new Command<string>((barcode) =>
        {
            try
            {
                Barcodes.Remove(barcode);
            }
            catch (System.Exception ex)
            {

                utils.SendErrorMessage(ex.Message);
            }
        });
    }
}
