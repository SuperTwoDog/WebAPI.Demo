using Common.Model;
using Entity.Models;
using IServices;
using Services.BASE;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// 用户信息管理数据操作类
    /// </summary>
    public class UserInfoService : BaseServices<UserInfoModel>, IUserInfoService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public UserInfoModel LogOn(QueryParams param)
        {
            string sql = "SELECT * FROM Sys_UserInfo WHERE IsActive = 1 AND Account=@account AND PasswordHash=@passwordHash";
            List<SugarParameter> paramList = new List<SugarParameter>();
            paramList.Add(new SugarParameter("@account", param.UserName));
            paramList.Add(new SugarParameter("@passwordHash", param.PassWord));
            List<UserInfoModel> dataList = base.ExecuteList(sql, paramList.ToArray());
            if (dataList.Count > 0)
            {
                return dataList[0];
            }
            return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserInfoModel> GetUserList()
        {
            string sql = "select * from Sys_UserInfo where account like @account ";
            List<SugarParameter> paramList = new List<SugarParameter>();
            paramList.Add(new SugarParameter("@account", "%wang%"));
            Pagination page = new Pagination();
            page.PageRows = 10;
            page.PageIndex = 1;
            return base.ExecuteList(sql, ref page, paramList.ToArray());
        }
    }
}
