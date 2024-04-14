using SqlSugar;

namespace Wms.Web.Api.Service.Model
{
    /// <summary>
    /// 数据源导入
    ///</summary>
    [SugarTable("v_Base_User")]
    public class VBaseUser
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_ID")]
        public long? F_ID { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_UserCode")]
        public string F_UserCode { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_UserName")]
        public string F_UserName { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_DeptID")]
        public long? F_DeptID { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_DeptCode")]
        public string F_DeptCode { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_DeptName")]
        public string F_DeptName { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_SapNo")]
        public string F_SapNo { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_LoginPwd")]
        public string F_LoginPwd { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_RelationRole")]
        public long? F_RelationRole { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_RoleName")]
        public string F_RoleName { get; set; } = "";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_OrderNo")]
        public int? F_OrderNo { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_IsEffective")]
        public string F_IsEffective { get; set; } = "use";
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "F_Remark")]
        public string F_Remark { get; set; } = "";
    }
}
