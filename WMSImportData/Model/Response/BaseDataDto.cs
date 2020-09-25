using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response
{
    /// <summary>
    /// 基础数据。
    /// </summary>
    public class BaseDataDto
    {
        /// <summary>
        /// 业务信息列表
        /// </summary>
        public List<BusinessInfoDto> businessList = new List<BusinessInfoDto>();

        /// <summary>
        /// 组织信息列表
        /// </summary>
        public List<OrganizationInfoDto> organizationList = new List<OrganizationInfoDto>();
    }
}
