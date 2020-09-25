using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsUserSystem.Dto.Response;
using TmsUserSystem.Dto.Response.Role;

namespace TmsUserSystem.Dto.Request
{
    public class CreateUserDto
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        public string loginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string realName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string portrait { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>

        public string userType { get; set; }
        /// <summary>
        /// 账号有效期
        /// </summary>

        public string validDate { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>

        public string deptName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>

        public string position { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>

        public string userStatus { get; set; }
        /// <summary>
        /// 权限级别
        /// </summary>

        public string rightsType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 业务列表
        /// </summary>
        public List<BusinessInfoDto> businessList { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<SimpleRoleInfoDto> roleList { get; set; }

        /// <summary>
        /// wms部门列表
        /// </summary>
        public List<WmsDepartmentDto> wmsDepartmentList { get; set; }


    }
}
