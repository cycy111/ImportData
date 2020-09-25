using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response
{
    /// <summary>
    /// wms仓库部门模型
    /// </summary>
    public class WmsDepartmentDto
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptName { get; set; }
    }
}
