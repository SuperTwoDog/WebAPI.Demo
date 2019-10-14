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
    /// 角色
    /// 2012-12-28
    /// </summary>
    [SugarTable("Sys_Role")]
    public class RoleModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public string RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// [a-zA-Z\s\.]{1,20}[0-9]*|[\u4e00-\u9fa5]{1,20}[0-9]*
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = "{0} 最少需要 {2} 个字符长度.", MinimumLength = 1)]
        [RegularExpression(@"[a-zA-Z\s\.\u4e00-\u9fa5]{1,20}[0-9]*", ErrorMessage = "角色名称只能为中文名或英文名，后可带数字")]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        /// <summary>
        /// 是否超级管理员角色
        /// </summary>
        public int? IsAdminRole { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int? IsActive { get; set; }

        /// <summary>
        /// 是否系统角色
        /// </summary>
        public int? IsSysRole { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUserID { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(256, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string Remark { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// 角色级别
        /// </summary>
        public int RoleLevel { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNO { get; set; }
    }
}
