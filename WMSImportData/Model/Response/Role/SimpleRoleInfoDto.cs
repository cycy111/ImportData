using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response.Role
{
    public class SimpleRoleInfoDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int roleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }
    }
}
