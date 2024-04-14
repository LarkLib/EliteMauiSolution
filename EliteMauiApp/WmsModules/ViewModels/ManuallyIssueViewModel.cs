using DevExpress.Maui.DataForm.Internal;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.WmsModules.Models;
using Elite.LMS.Maui.WmsModules.Services;
using Java.Lang;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Elite.LMS.Maui.ViewModels;

public class ManuallyIssueViewModel : BaseViewModel
{
    public PlanBody IssuePlanSelectedItem { get; set; }
    public string MaterialBarcode { get; set; }
    public ICommand CheckPlanCommand { get; private set; }
    public ICommand ExecutePlanCommand { get; private set; }

    private int WarehouseId { get; set; }

    public ManuallyIssueViewModel(int warehouseId)
    {
        WarehouseId = warehouseId;
        CheckPlanCommand = new Command(async () =>
        {
            try
            {
                var checkInfoBuilder = new StringBuilder($"物料条码{MaterialBarcode}验证");
                var confirmed = await utils.SendConfirmMessage("请确认是否提信息！");
                if (confirmed != true) return;
                var ret = WmsService.ManuallyIssueMaterialCheck(MaterialBarcode);
                if (!ret.Code.Equals("200"))
                {
                    checkInfoBuilder.Append($"失败。{ret.Msg}");
                    throw new System.ApplicationException(checkInfoBuilder.ToString());
                }
                checkInfoBuilder.Append($"成功。{ret.Msg}");
                utils.SendInformationMessage(checkInfoBuilder.ToString());
            }
            catch (System.Exception ex)
            {
                utils.SendErrorMessage(ex.Message);
            }
        });
        ExecutePlanCommand = new Command<string>(async (localpLocalPlanCode) =>
        {
            try
            {
                var confirmed = await utils.SendConfirmMessage("请确认是否提信息！");
                if (confirmed != true) return;
                var ret = WmsService.ExecuteManuallyIssuePlan(new PlanExecuteRequestBody
                {
                    LocalPlanCode = localpLocalPlanCode,
                    UserID = LoginUser.Instance.UserId,
                    UserCode = LoginUser.Instance.UserCode,
                    UserName = LoginUser.Instance.UserName
                });
                if (!ret.Code.Equals("200")) throw new System.ApplicationException(ret.Msg);
            }
            catch (System.Exception ex)
            {

                utils.SendErrorMessage(ex.Message);
            }
        });
        PullToRefreshCommand = new Command(ExecutePullToRefreshCommand);
    }

    public void GetIssuePlanData()
    {
        var retIssuePlans = WmsService.GetManuallyIssuePlan(WarehouseId);
        if (retIssuePlans != null && retIssuePlans.Code.Equals("200")) issuePlans = retIssuePlans.Data.ToList();
    }

    #region Pull Refersh

    IList<PlanBody> issuePlans;
    public IList<PlanBody> IssuePlans
    {
        get
        {
            GetIssuePlanData();
            return issuePlans;
        }
        set
        {
            if (issuePlans != value)
            {
                issuePlans = value;
                OnPropertyChanged("IssuePlans");
            }
        }
    }
    bool isRefreshing = false;
    public bool IsRefreshing
    {
        get { return isRefreshing; }
        set
        {
            if (isRefreshing != value)
            {
                isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }
    }

    ICommand pullToRefreshCommand = null;
    public ICommand PullToRefreshCommand
    {
        get { return pullToRefreshCommand; }
        set
        {
            if (pullToRefreshCommand != value)
            {
                pullToRefreshCommand = value;
                OnPropertyChanged("PullToRefreshCommand");
            }
        }
    }

    void ExecutePullToRefreshCommand()
    {
        Task.Run(() =>
        {
            GetIssuePlanData();
            IsRefreshing = false;
        });
    }
    #endregion
}