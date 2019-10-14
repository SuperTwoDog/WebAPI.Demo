using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    /// 公用查询条件类
    /// </summary>
    public class QueryParams
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 关键字1
        /// </summary>
        public string Keyword1 { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UsesID { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTIme { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Sattus { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public string TypeID { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public string AreaID { get; set; }

        /// <summary>
        /// 团队ID
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsAudit { get; set; }

        /// <summary>
        /// 是否导出
        /// </summary>
        public bool IsExport { get; set; }

        /// <summary>
        /// 计量分类ID
        /// </summary>
        public string SubjectTypeID { get; set; }

        /// <summary>
        /// 讲师ID
        /// </summary>
        public string LecturerID { get; set; }

        /// <summary>
        /// 计划ID
        /// </summary>
        public string PlanID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
