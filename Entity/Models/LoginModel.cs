using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginModel
    {
        #region 属性

        /// <summary>
        /// 应用程序唯一标志
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 手持设备唯一标志
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码(32位MD5小写加密)
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}
