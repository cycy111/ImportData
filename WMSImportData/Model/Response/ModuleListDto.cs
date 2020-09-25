using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response
{
    public class ModuleListDto
    {

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleNo { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 访问路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Modifier{ get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string ModifyDate { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string iconName { get; set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public string controller { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string action { get; set; }

        

 
 
        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }



    }
}
