using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    /// 树菜单
    /// </summary>
    public class TreeViewModel
    {
        /// <summary>
        /// 树ID
        /// </summary>
        public string TreeID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string TreeName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string TreeUrl { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNO { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public string Fonts { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 子菜单树
        /// </summary>
        public List<TreeViewModel> ChildTree { get; set; }
    }
}
