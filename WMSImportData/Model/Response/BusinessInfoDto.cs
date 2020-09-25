using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response
{
    public class BusinessInfoDto
    {
        /// <summary>
        /// 业务Id
        /// </summary>
        public long businessId { get; set; }

        /// <summary>
        /// 业务代码
        /// </summary>
        public string businessCode { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string businessName { get; set; }
    }
}
