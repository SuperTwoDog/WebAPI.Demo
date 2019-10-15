using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    [SugarTable("Sys_MenuInfo")]
    public class MenuInfoModel
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public string MenuInfoID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string MenuName { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public string Fonts { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 是否检查
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsActived { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNO { get; set; }

    }
}
