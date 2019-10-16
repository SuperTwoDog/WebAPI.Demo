using Common.Model;
using Entity.Models;
using IServices;
using Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// 菜单信息管理数据操作类
    /// </summary>
    public class MenuInfoService : BaseServices<MenuInfoModel>, IMenuInfoService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public List<MenuInfoModel> GetMenuList()
        {
            string sql = "select * from sys_menuinfo where isActived = 1 ";
            return base.ExecuteList(sql);
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        public List<MenuInfoModel> GetMenuList(QueryParams param)
        {
            List<MenuInfoModel> menuList = base.Query().FindAll(x => x.IsActived == 1);
            return menuList;
        }
    }
}
