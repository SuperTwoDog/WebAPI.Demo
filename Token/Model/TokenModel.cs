using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Token.Model
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Roles { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public List<string> Users { get; set; }

        /// <summary>
        /// 口令过期时间
        /// </summary>
        public DateTime? ExpiryDateTime { get; set; }
    }
}
