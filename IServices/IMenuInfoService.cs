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
    /// 菜单访问接口
    /// </summary>
    public interface IMenuInfoService : IBaseServices<MenuInfoModel>
    {
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        List<MenuInfoModel> GetMenuList();

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        List<MenuInfoModel> GetMenuList(QueryParams param);
    }
}
