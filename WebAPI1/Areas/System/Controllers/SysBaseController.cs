using Common.Helper;
using Common.Model;
using Entity.Models;
using Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.AuthAttributes;

namespace WebAPI.Areas.System.Controllers
{
    /// <summary>
    /// 系统基础信息管理API控制器
    /// </summary>
    public class SysBaseController : ApiController
    {
        #region 数据访问接口
        public UserInfoService userService { get; set; }
        public RoleService roleService { get; set; }
        public MenuInfoService menuService { get; set; }
        #endregion

        #region 获取数据

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表分页数据
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Dev, Time = "2019-10-16")]
        [ApiAuthorize(Roles = "Admin,超级管理员,Common,业务员")]
        public dynamic GetMenuList(QueryParams param)
        {
            //if (param.PageSize > 0)
            //{
            //    var 
            //}
            //menuService.QueryEx()
            return null;
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Dev, Time = "2019-10-16")]
        //[ApiAuthorize(Roles = "Admin,超级管理员,Common,业务员")]
        public dynamic GetAllMenuList()
        {
            List<MenuInfoModel> menuList = menuService.GetMenuList(new QueryParams());
            List<TreeViewModel> treeList = ConvertTreeData(menuList);
            return TreeHelper.GetChildTree(treeList);
        }

        private List<TreeViewModel> ConvertTreeData(List<MenuInfoModel> menuList)
        {
            List<TreeViewModel> treeList = new List<TreeViewModel>();
            foreach (var item in menuList)
            {
                TreeViewModel model = new TreeViewModel()
                {
                    TreeID = item.MenuInfoID,
                    TreeName = item.MenuName,
                    TreeUrl = item.Url,
                    ParentID = item.ParentID,
                    OrderNO = item.OrderNO,
                    Icon = item.Icon,
                    Spread = item.Spread
                };
            }
            return treeList;
        }

        #endregion

        #endregion

        #region 提交数据

        #endregion
    }
}
