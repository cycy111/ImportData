using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.Config
{
    /// <summary>
    /// 数据库连接配置映射类
    /// </summary>
    public class DbConnections
    {
        /// <summary>
        /// WMSReport数据库
        /// </summary>
        public string WMSReport { get; set; }

        /// <summary>
        /// 数据库集合
        /// </summary>
        public WmsDb wmsDb { get; set; } = new WmsDb();

    }

    /// <summary>
    /// wms数据库连接配置映射类
    /// </summary>
    public class WmsDb
    {
        public string server { get; set; }

        public string uid { get; set; }

        public string pwd { get; set; }
    }
}
