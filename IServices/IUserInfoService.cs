using Common.Model;
using Entity.Models;
using IServices.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public interface IUserInfoService : IBaseServices<UserInfoModel>
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        List<UserInfoModel> GetUserList();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        UserInfoModel LogOn(QueryParams param);
    }
}
