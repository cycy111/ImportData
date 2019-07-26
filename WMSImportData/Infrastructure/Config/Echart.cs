using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.Config
{
    /// <summary>
    /// 曲线图基础配置
    /// </summary>
    public class Echart
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 标题值
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// x轴
        /// </summary>
        public string[] xAxis { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<EchartData> series { get; set; }

        /// <summary>
        /// 图例
        /// </summary>
        public List<string> legend { get; set; } = new List<string>();
    }
}
