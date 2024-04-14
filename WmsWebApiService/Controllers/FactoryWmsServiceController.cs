using System;
using System.Data;
using System.Web.Http;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// 厂级WMS相关服务接口
    /// </summary>
    [RoutePrefix("api/FactoryWmsService")]
    public class FactoryWmsServiceController : ApiController
    {
        #region ---成品备货申请
        /// <summary>
        /// 厂级Wms成品备货申请
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PrepareProductReq")]
        public WmsResponseBody PrepareProductReq([FromBody] ProductPrepareRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body != null)
                {
                    if (body.MaterialList == null)
                        throw new Exception("物料信息不能为空.");

                    //获取成品备货信息
                    DataTable table = CommonFunc.GetPrepareProductDataTableScheme();
                    foreach (var material in body.MaterialList)
                    {
                        DataRow row = table.NewRow();
                        row["F_MaterialCode"] = material.MaterialCode;
                        row["F_Qty"] = material.Qty;
                        table.Rows.Add(row);
                    }

                    if (table.Rows.Count == 0)
                        throw new Exception("成品备货信息不能为空.");

                    //生成成品备货计划
                    CommonFunc.CreateProductPreparePlan(body.TaskCode, table);
                }
                else
                {
                    response.TaskCode = "";
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.TaskCode = body.TaskCode;
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion

        #region ---成品出库信息上报
        /// <summary>
        /// 成品出库信息上报
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReportProductOutbound")]
        public WmsResponseBody ReportProductOutbound([FromBody] ReportProductOutboundBody body)
        {
            WmsResponseBody response = new WmsResponseBody();            

            return response;
        }
        #endregion

        #region ---成品上架或直接出库申请
        /// <summary>
        /// 成品上架申请
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductShelvesOrOutReq")]
        public WmsResponseBody ProductShelvesOrOutReq([FromBody] ProductMoveOrOutReqBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body != null)
                {
                    if (string.IsNullOrWhiteSpace(body.ReqType))
                        throw new Exception("申请类型不能为空.");

                    string reqType = body.ReqType.Trim().ToLower();
                    if (!reqType.Equals("up") && !reqType.Equals("out"))
                        throw new Exception($"未知的申请类型值 {body.ReqType}.");

                    //获取成品条码信息
                    DataTable table = CommonFunc.GetGroupBoxBarcodeDataTableScheme();
                    foreach (var barcode in body.BarcodeList)
                    {
                        DataRow row = table.NewRow();
                        row["F_Barcode"] = barcode.Barcode;
                        table.Rows.Add(row);
                    }

                    if (table.Rows.Count > 0)
                    {
                        //提交成品上架存储或直接出库
                        CommonFunc.SubmitProductShelvesOrOut(table, reqType);
                    }
                }
                else
                {
                    response.TaskCode = "";
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.TaskCode = "";
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion
    }
}
