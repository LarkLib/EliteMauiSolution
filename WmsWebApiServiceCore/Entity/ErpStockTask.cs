using Newtonsoft.Json;

namespace WmsWebApiService.Entity
{
    public class ErpStockTaskBody
    {
        public string? PallectCode { get; set; }
        public string? PalletCode { get; set; }
        public string? PalletType { get; set; }
        public string TaskType { get; set; }
        public string TaskCode { get; set; }
        public string StationID { get; set; }
        public string MaterialCode { get; set; }
        public string Quantity { get; set; }
    }
}
