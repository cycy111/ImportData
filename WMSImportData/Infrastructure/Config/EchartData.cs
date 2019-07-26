using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.Config
{
    /// <summary>
    /// 曲线图左边数据
    /// </summary>
    public class EchartData
    {
        /// <summary>
        /// Y值
        /// </summary>
        public List<string> data { get; set; }

        /// <summary>
        /// X值
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 类型 1.入库；2出库
        /// </summary>
        public string countType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string datetime { get; set; }
    }
}
