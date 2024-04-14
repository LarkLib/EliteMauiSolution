using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// MES请求物料返库任务信息实体类
    /// </summary>
    public class MesRequestReturnTaskBody
    {
        /// <summary>
        /// 申请任务号
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 申请机台号
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// 机台关联的输送线编号
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 物料明细信息
        /// </summary>
        public List<MesRequestMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// MES请求物料自动出库任务信息实体类
    /// </summary>
    public class MesRequestOutboundTaskBody
    {
        /// <summary>
        /// 申请任务号
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 申请机台号
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 任务明细信息
        /// </summary>
        public List<MesRequestTaskDetailBody> TaskList { get; set; }
    }

    /// <summary>
    /// Mes请求出库任务明细信息实体类
    /// </summary>
    public class MesRequestTaskDetailBody
    {
        /// <summary>
        /// 机台关联的输送线编号
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// 物料明细信息
        /// </summary>
        public List<MesRequestMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 物料信息实体类
    /// </summary>
    public class MesRequestMaterialBody
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 物料条码
        /// </summary>
        public string MaterialBarcode { get; set; } = "";
        /// <summary>
        /// 出库申请：申请数量；返库申请：WMS送料时传入的数量
        /// </summary>
        public decimal Qty { get; set; } = 0;
        /// <summary>
        /// 物料消耗数量
        /// </summary>
        public decimal ConsumedQty { get; set; } = 0;
        /// <summary>
        /// 空轴标识（导线和端子用）；Y: 空   N: 非空；如果是模具，则默认填写'N'。
        /// </summary>
        public string EmptyFlag { get; set; } = "N";
        /// <summary>
        /// 包装规格；成品入库时用
        /// </summary>
        public string MaterialPS { get; set; } = "";
    }

    /// <summary>
    /// Mes请求更改物料库存数量实体类
    /// </summary>
    public class MesRequestChangeInventoryBody
    {
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 申请信息
        /// </summary>
        public List<ChangeInventoryBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 要更改库存的明细信息
    /// </summary>
    public class ChangeInventoryBody
    {
        /// <summary>
        /// 物料条码
        /// </summary>
        public string MaterialBarcode { get; set; } = "";
        /// <summary>
        /// 出库申请：申请数量；返库申请：WMS送料时传入的数量
        /// </summary>
        public decimal Qty { get; set; } = 0;
    }
}