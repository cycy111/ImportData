using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.DbCommon
{
    public interface IWmsDbConnection
    {
        string GetDbConnStr(string deptCode, string deptName);
    }
}
