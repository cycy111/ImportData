using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmsReport.Infrastructure.Config;
using Dapper;
using Zh.Common.DbHelper;
namespace WmsReport.Infrastructure.DbCommon
{
    /// <summary>
    /// 在DbConnection基础上再封装一层，实现动态配置数据库链接字符串，以达到访问不能的库
    /// </summary>
    public class WmsDbConnection: IWmsDbConnection
    {
        //实现注入，基础数据库连接，配合控制台入口映射实现访问数据配置文件DbConnections
        private IOptions<DbConnections> _dbConnections;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnsAccessor"></param>
        public WmsDbConnection(IOptions<DbConnections> dbConnsAccessor)
        {
            _dbConnections = dbConnsAccessor;
        }

        /// <summary>
        /// 根据部门，动态配置访问不同数据库方法
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public string GetDbConnStr(string deptCode,string deptName)
        {
            string server = _dbConnections.Value.wmsDb.server;
            string uid = _dbConnections.Value.wmsDb.uid;
            string pwd = _dbConnections.Value.wmsDb.pwd;
            string database = getDatabase(deptCode, deptName);
            return $"server={server};uid= {uid};pwd={pwd};database={database};";
        }

        private string getDatabase(string deptCode,string deptName)
        {
            string result = "";
            string server = _dbConnections.Value.wmsDb.server;
            string uid = _dbConnections.Value.wmsDb.uid;
            string pwd = _dbConnections.Value.wmsDb.pwd;
            string dbStr = _dbConnections.Value.WMSReport;
            using (var conn = DapperHelper.GetOpenConnection(dbStr, DbProvider.SqlServer))
            {
                string sql = string.Format("select DbName  from s_Department where DeptCode=@deptCode");
                result = conn.ExecuteScalar<string>(sql, new { deptCode = deptCode }); //conn.Query<string>(sql).ToList()[0];
            }
        
            return result;

        }

    }
}
