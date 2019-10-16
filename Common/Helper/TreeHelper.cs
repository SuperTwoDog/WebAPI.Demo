using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    /// <summary>
    /// 树菜单帮助类
    /// </summary>
    public class TreeHelper
    {
        /// <summary>
        /// 添加树菜单子菜单
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="treeList"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetChildList(string parentID,List<TreeViewModel> treeList)
        {
            var childList = treeList.FindAll(x => x.TreeID == parentID);
            return childList;
        }

        /// <summary>
        /// 获取树菜单列表信息
        /// </summary>
        /// <param name="treeList"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetChildTree(List<TreeViewModel> treeList)
        {
            List<TreeViewModel> list = new List<TreeViewModel>();
            foreach (var item in treeList)
            {
                item.ChildTree = GetChildList(item.TreeID, treeList);
                list.Add(item);
            }
            return list;
        }
    }
}
