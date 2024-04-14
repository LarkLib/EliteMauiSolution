using DevExpress.Maui.DataForm.Internal;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Services;
using Elite.LMS.Maui.Views;
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

public class MaterialGroupBoxViewModel : BaseViewModel
{
    public PlanBody IssuePlanSelectedItem { get; set; }
    public string MaterialBarcode { get; set; }
    public ICommand BoxingPlanCommand { get; private set; }
    private int WarehouseId { get; set; }

    public MaterialGroupBoxViewModel(int warehouseId)
    {
        WarehouseId = warehouseId;
        BoxingPlanCommand = new Command<string>(async (localpLocalPlanCode) =>
        {
            try
            {
                await NavigationService.NavigateToPage(new MaterialGroupBoxExecuteView(localpLocalPlanCode));
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
