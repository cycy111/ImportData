using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using TmsUserSystem.Dto.Request.Module;
//using TmsUserSystem.Model.Role;

namespace TmsUserSystem.Dto.Response.Role
{
    public class RoleInfoDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int roleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public string roleType { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 角色备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string roleDesc { get; set; }

        /// <summary>
        /// 角色创建者ID
        /// </summary>
        public long creator { get; set; }

        /// <summary>
        /// 角色创建者姓名
        /// </summary>
        public string creatorName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public long modifier { get; set; }

        /// <summary>
        /// 修改者姓名
        /// </summary>
        public string modifierName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string modifyDate { get; set; }

        /// <summary>
        /// 模块列表
        /// </summary>
        //public List<SimpleModuleInfoDto> moduleList { get; set; }

        ///// <summary>
        ///// Tms业务列表
        ///// </summary>
        //public List<RoleBusiness> businessList { get; set; }

        ///// <summary>
        ///// wms部门列表
        ///// </summary>
        //public List<RoleWmsDepartment> wmsDepartmentList { get; set; }
    }
}
