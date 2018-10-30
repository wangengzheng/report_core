using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report_NetCore.Models.RequestModels
{
    public class GetJsonResultRequestModel
    {
        /// <summary>
        /// 当前的页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 当前显示的行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 总记录数 /求个的字段
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// 当前查询的字符串
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// report
        /// </summary>
        public string Report { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 当前排序列字段名
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }

        ///// <summary>
        ///// easyui fiter功能
        ///// </summary>
        //public string filterRules { get; set; }

        /// <summary>
        /// filter对应的值
        /// </summary>
        //public List<Condition> condition { get; set; }

        public string Limitvalue { get; set; }
    }
}
