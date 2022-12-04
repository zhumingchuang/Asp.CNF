using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Models.Configs
{
    /// <summary>
    /// 菜单按钮配置
    /// </summary>
    public class Button
    {
        /// <summary>
        /// 详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 导出
        /// </summary>
        public string Export { get; set; }
        /// <summary>
        /// 导入
        /// </summary>
        public string Import { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public string Delete { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>
        public string Edit { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        public string Add { get; set; }
        /// <summary>
        /// 授权
        /// </summary>
        public string Auth { get; set; }

    }
}
