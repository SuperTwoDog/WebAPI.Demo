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
    /// 用户信息管理
    /// </summary>
    [SugarTable("Sys_UserInfo")]
    public class UserInfoModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public string UserID { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^[a-zA-Z0-9_]{2,20}$", ErrorMessage = "登录帐号必须为字母、数字、下划线；2位到20位字符长度")]
        [Display(Name = "登录帐号")]
        [SugarColumn(Length = 60, IsNullable = true)]
        public string Account { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = "{0} 最少需要 {2} 个字符长度.", MinimumLength = 1)]
        [RegularExpression(@"[a-zA-Z\s\.\[\]]{1,20}[0-9]*|[\u4e00-\u9fa5\[\]]{1,10}[0-9]*", ErrorMessage = "用户姓名只能为中文、英文名或[中括号]，后可带数字")]
        [Display(Name = "用户姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        //[Required(ErrorMessage = "请输入{0}")]
        [StringLength(30, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.-]+)@([\da-zA-Z\.-]+)\.([a-zA-Z\.]{2,6})$", ErrorMessage = "请填写有效邮箱地址")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "邮箱地址")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = "{0} 最少需要 {2} 个字符长度.", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9_]{6,20}$", ErrorMessage = "密码必须为字母、数字、下划线；6位到20位字符长度")]
        [Display(Name = "密码")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [Display(Name = "单位名称")]
        [StringLength(256, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        [Display(Name = "通讯地址")]
        [StringLength(256, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string ContactAddress { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话")]
        [RegularExpression(@"^[0-9-]{0,32}$", ErrorMessage = "{0} 只能由数字或-符号组成；0位到30位字符长度")]
        [StringLength(30, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string Telephone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        public string Fax { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 分机号
        /// </summary>
        [Display(Name = "分机号")]
        public string PhoneExt { get; set; }

        /// <summary>
        /// 个人头像路径
        /// </summary>
        [Display(Name = "个人头像路径")]
        [StringLength(256, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string PicturePath { get; set; }

        /// <summary>
        /// 电子签名
        /// </summary>
        [Display(Name = "电子签名")]
        [StringLength(256, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string E_Signature { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        //[Display(Name = "是否启用")]
        public int IsActive { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public string ModifyUserID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 密码更新时间
        /// </summary>
        //public DateTime? PwdTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(256, ErrorMessage = "{0} 不能超过 {1} 个字符长度.")]
        public string Remark { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public string AreaID { get; set; }

        /// <summary>
        /// 客户管理权限(0.验证公司名称地址及联系方式1.只验证公司名称地址存在2.公司名称地址不同时相同)
        /// </summary>
        public int GuestRole { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNO { get; set; }

        /// <summary>
        /// 人员资质
        /// </summary>
        public string Qualifications { get; set; }

        /// <summary>
        /// 团队ID
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        /// 微信ID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 密码修改时间
        /// </summary>
        public DateTime? PwdTime { get; set; }

        public string RoleID { get; set; }
    }
}
