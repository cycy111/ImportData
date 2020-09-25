using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsUserSystem.Dto.Response.Role;

namespace TmsUserSystem.Dto.Response
{
    public class UserInfoDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long userId { get; set; } 
        /// <summary>
        /// 登陆名
        /// </summary>
        public string loginName { get; set; } 
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
        /// 手机号是否验证
        /// </summary>
        public int isMobileVerified { get; set; } 
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; } 
        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        public int isEmailVerified { get; set; } 
        /// <summary>
        /// 头像
        /// </summary>
        public string portrait { get; set; } 

        /// <summary>
        /// 登陆日期
        /// </summary>
        public string loginDate { get; set; }

        /// <summary>
        /// 最后一次登陆日期
        /// </summary>
        public string lastLoginDate { get; set; } 

        /// <summary>
        /// 登陆次数
        /// </summary>
        public int loginCount { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public string editDate { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string userType { get; set; }

        public string userTypeDesc { get; set; }


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

        public string userStatusDesc { get; set; }


        /// <summary>
        /// 权限级别
        /// </summary>
        public string rightsType { get; set; }

        public string rightsTypeDesc { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建者的姓名
        /// </summary>
        public string creatorName { get; set; }

        /// <summary>
        /// 修改者的姓名
        /// </summary>
        public string modifierName { get; set; }


        /// <summary>
        /// 业务列表
        /// </summary>
        public List<BusinessInfoDto> businessList { get; set; }

        /// <summary>
        /// 所拥有的业务代码，"DX1Q01,DX2Q02"
        /// </summary>
        public string businessCodes { get; set; }

        /// <summary>
        /// 所拥有的业务名称，"东西一区,东西二区"
        /// </summary>
        public string businessNames { get; set; }

        /// <summary>
        /// 所拥有的业务Id，"318,210"
        /// </summary>
        public string businessIds { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<SimpleRoleInfoDto> roleList { get; set; }

        /// <summary>
        /// wms部门列表
        /// </summary>
        public List<WmsDepartmentDto> wmsDepartmentList { get; set; }

        /// <summary>
        /// 该用户拥有的所有wms部门名称，逗号分隔
        /// </summary>
        public string wmsDepartmentNames { get; set; }

        /// <summary>
        /// 所拥有的角色名，"调度员，订单员"
        /// </summary>
        public string roleNames { get; set; }

        /// <summary>
        /// 登陆令牌
        /// </summary>
        public string token { get; set; } //登陆令牌

        /// <summary>
        /// 该用户在wms系统中的部门编号
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 该用户在wms系统中的用户ID
        /// </summary>
        public int wmsUserSign { get; set; }

        /// <summary>
        /// 用户所在wms系统给数据库名。
        /// </summary>
        public string wmsDbName { get; set; }

        /// <summary>
        /// Wms系统的角色
        /// </summary>
        public string wmsRole { get; set; }

    }
}
