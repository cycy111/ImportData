using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsUserSystem.Dto.Response
{
    public class MenuListDto
    {
        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleNo { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 父级模块编号
        /// </summary>
        public string ParentNo { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Levels { get; set; }
        /// <summary>
        /// 是否是目录
        /// </summary>
        public int Isdirecory { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        
    }
}
