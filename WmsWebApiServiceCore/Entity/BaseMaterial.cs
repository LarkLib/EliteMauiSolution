using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace WmsWebApiService.Entity
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("T_Base_Material")]
    public class BaseMaterial
    {
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_ID" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public long F_ID { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_MaterialCode"    )]
         public string F_MaterialCode { get; set; }
        /// <summary>
        ///  
        /// 默认值: ('SCM')
        ///</summary>
         [SugarColumn(ColumnName="F_Pattern"    )]
         public string F_Pattern { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_Spec"    )]
         public string F_Spec { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_Level"    )]
         public string F_Level { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_LoadIndex"    )]
         public string F_LoadIndex { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_Board"    )]
         public string F_Board { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_Piece"    )]
         public string F_Piece { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_MouldSeries"    )]
         public long? F_MouldSeries { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_TrademarkID"    )]
         public long? F_TrademarkID { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_ElectroplateFlag"    )]
         public string F_ElectroplateFlag { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_MouldCategory"    )]
         public long? F_MouldCategory { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_ReceiveCell"    )]
         public long? F_ReceiveCell { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="F_Remark"    )]
         public string F_Remark { get; set; }
        /// <summary>
        ///  
        /// 默认值: (getdate())
        ///</summary>
         [SugarColumn(ColumnName="F_CreateTime"    )]
         public DateTime F_CreateTime { get; set; }
    }
}
