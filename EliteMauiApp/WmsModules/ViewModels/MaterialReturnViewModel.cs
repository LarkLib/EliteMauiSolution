using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.WmsModules.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace Elite.LMS.Maui.ViewModels;

public class MaterialReturnViewModel : BaseViewModel
{
    public WarehouseBody[] Warehouses { get; private set; }
    public WarehouseBody WarehouseSelectedItem { get; set; }
    public StationBody StationSelectedItem { get; set; }
    public string MaterialBarcode { get; set; }
    public ObservableCollection<StationBody> Stations { get; private set; } = new ObservableCollection<StationBody>();

    public ICommand WharehouseSelectionChangedCommand { get; private set; }
    public ICommand SubmitCommand { get; private set; }

    public MaterialReturnViewModel()
    {
        WharehouseSelectionChangedCommand = new Command(() =>
        {
            Stations.Clear();
            var retstations = WmsService.GetStationByWarehouse(WarehouseSelectedItem.WarehouseID, null);
            if (retstations != null && retstations.Code.Equals("200"))
            {
                foreach (var item in retstations.Data)
                {
                    Stations.Add(item);
                }
            }
        });
        SubmitCommand = new Command(async () =>
        {
            try
            {
                var confirmed = await utils.SendConfirmMessage("请确认是否提交初始入库信息！");
                if (confirmed != true) return;
                var ret = WmsService.MaterialReturnInbound(new MaterialInboundRequestBody
                {
                    MaterialBarcode = MaterialBarcode,
                    StationID = StationSelectedItem.StationID,
                    WarehouseID = WarehouseSelectedItem.WarehouseID,
                    OperatorID = LoginUser.Instance.UserId
                });
                if (!ret.Code.Equals("200")) throw new System.ApplicationException(ret.Msg);

            }
            catch (System.Exception ex)
            {

                utils.SendErrorMessage(ex.Message);
            }
        });

        InitData();
    }

    public void InitData()
    {
        var retWarehouse = WmsService.GetWarehouse();
        if (retWarehouse != null && retWarehouse.Code.Equals("200")) Warehouses = retWarehouse.Data.ToArray();
    }
}
