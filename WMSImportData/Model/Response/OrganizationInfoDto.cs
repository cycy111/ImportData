using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response
{
    public class OrganizationInfoDto
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public long organizationId { get; set; }

        /// <summary>
        /// 组织代码
        /// </summary>
        public string organizationCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string organizationName { get; set; }

        /// <summary>
        /// 是否是总部
        /// </summary>
        public bool isHq { get; set; }
    }
}
