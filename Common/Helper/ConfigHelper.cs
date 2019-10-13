using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    /// <summary>
    /// Config文件操作类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            var r = ConfigurationManager.AppSettings[key];
            if (r == null)
            {
                return "";
            }
            else
            {
                return r.ToString().Trim();
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnString(string key)
        {
            var r = ConfigurationManager.ConnectionStrings[key];
            if (r == null)
            {
                return "";
            }
            else
            {
                return r.ToString().Trim();
            }
        }
    }
}
