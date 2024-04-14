using EliteHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// 垂直升降库相关服务接口
    /// </summary>
    [RoutePrefix("api/LiftWmsService")]
    public class LiftWmsServiceController : ApiController
    {
        /// <summary>
        /// 库存同步服务接口
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SyncInventory")]
        public WmsResponseBody SyncInventory([FromBody] LiftSyncInventoryBody body)
        {
            WmsResponseBody re = new WmsResponseBody()
            {
                TaskCode = ""
            };

            try
            {
                if (body == null)
                {
                    re.Code = "100";
                    re.Msg = "Json序列化失败";
                }
                else
                {
                    DataTable tableInventory = CommonFunc.GetLiftInventoryDataTableScheme();
                    foreach (var material in body.MaterialList)
                    {
                        DataRow row = tableInventory.NewRow();
                        row["F_MaterialCode"] = material.MaterialCode;
                        row["F_MaterialName"] = material.MaterialName;
                        row["F_MaterialBarcode"] = material.MaterialBarcode;
                        row["F_Qty"] = material.Qty;
                        row["F_BatchNo"] = material.BatchNo;
                        tableInventory.Rows.Add(row);
                    }
                    
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@inventory", SqlDbType.Structured),
                        new SqlParameter("@type",SqlDbType.NVarChar, 255),
                        new SqlParameter("@message",SqlDbType.NVarChar, 500),
                        new SqlParameter("@return",SqlDbType.NVarChar, 50)
                    };

                    parameters[0].Value = tableInventory;
                    parameters[1].Value = body.TaskType;
                    parameters[2].Direction = ParameterDirection.Output;
                    parameters[3].Direction = ParameterDirection.ReturnValue;

                    SqlDbHelper.ExecuteNonQuery("Elite_P_System_SyncLiftInventory", parameters);

                    re.Code = parameters[3].Value.ToString();
                    re.Msg = parameters[3].Value.ToString().Equals("200") ? "succeed" : parameters[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                re.Code = "300";
                re.Msg = ex.Message;
            }

            return re;
        }

        /// <summary>
        /// 获取物料基础信息服务接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialQuery")]
        public MaterialQueryResponseBody MaterialQuery()
        {
            MaterialQueryResponseBody queryBody = new MaterialQueryResponseBody();

            try
            {
                DataTable table = SqlDbHelper.GetDataSet("select * from v_Base_Material where F_MaterialCategoryID in (3,4,5)").Tables[0] ?? 
                    throw new Exception("获取物料基础信息失败，请检查网络连接或与开发人员联系.");

                List<LocalMaterialBody> materialBodies = new List<LocalMaterialBody>();
                foreach (DataRow row in table.Rows)
                {
                    LocalMaterialBody materialBody = new LocalMaterialBody
                    {
                        MaterialCode = row["F_MaterialCode"].ToString(),
                        MaterialName = row["F_MaterialName"].ToString(),
                        MaterialSpec = row["F_Spec"] != DBNull.Value ? row["F_Spec"].ToString() : "",
                        MaterialUnit = row["F_Unit"] != DBNull.Value ? row["F_Unit"].ToString() : "",
                        MaterialCategoryID = row["F_MaterialCategoryID"].ToString(),
                        MaterialCategoryName = row["F_MaterialCategoryName"].ToString(),
                        MaterialValidity = row["F_MaterialValidity"].ToString()
                    };
                    materialBodies.Add(materialBody);
                }

                queryBody.Code = "200";
                queryBody.Msg = "succeed";
                queryBody.MaterialList = materialBodies;
            }
            catch (Exception ex)
            {
                queryBody.Code = "500";
                queryBody.Msg = ex.Message;
                queryBody.MaterialList = null;
            }
            
            return queryBody;
        }
    }
}
