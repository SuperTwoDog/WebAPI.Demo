using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common
{
    public static class ListExtension
    {
        /// <summary>
        /// DataSet转换为SelectListItem泛型集合
        /// </summary>
        /// <param name="ds">DataSet数据集</param>
        /// <param name="valueField">值字段</param>
        /// <param name="textField">显示字段</param>
        /// <param name="showAll">显示选择所有</param>
        /// <param name="selectedID">选中值</param>
        /// <param name="distinct">是否去除重复的行</param>
        /// <returns>返回SelectListItem泛型集合</returns>
        public static List<SelectListItem> ToSelectList<T>(this List<T> list, string valueField, string textField, bool showAll = false, string selectedID = "", bool distinct = false)
        {
            List<SelectListItem> list0 = new List<SelectListItem>();
            T _t = (T)Activator.CreateInstance(typeof(T));
            if (showAll)
            {
                SelectListItem item = new SelectListItem();
                item.Text = "==请选择==";
                item.Value = "-1";
                list0.Add(item);
            }
            PropertyInfo[] propertys = _t.GetType().GetProperties();
            foreach (T item in list)
            {
                SelectListItem item0 = new SelectListItem();
                int i = 0;
                foreach (PropertyInfo pi in propertys)
                {
                    if (pi.Name == valueField)
                    {
                        i++;
                        item0.Value = Convert.ToString(pi.GetValue(item, null) ?? DBNull.Value);
                        if (item0.Value == selectedID)
                        {
                            item0.Selected = true;
                        }
                    }
                    if (pi.Name == textField)
                    {
                        if (pi.Name != valueField)
                        {
                            i++;
                        }
                        item0.Text = Convert.ToString(pi.GetValue(item, null) ?? DBNull.Value);
                    }
                    if (i == 2)
                    {
                        break;
                    }
                }
                if (distinct && list0.FindIndex(x => x.Value == item0.Value) >= 0)
                {
                    continue;
                }
                list0.Add(item0);
            }
            return list0;
        }

        /// <summary>
        /// DataSet转换为SelectListItem泛型集合
        /// </summary>
        /// <param name="ds">DataSet数据集</param>
        /// <param name="valueField">值字段</param>
        /// <param name="textField">显示字段</param>
        /// <param name="showAll">显示选择所有</param>
        /// <param name="selectedID">选中值</param>
        /// <param name="distinct">是否去除重复的行</param>
        /// <returns>返回SelectListItem泛型集合</returns>
        public static List<SelectListItem> ToFinSelectList<T>(this List<T> list, string valueField, string textField, bool showAll = false, string selectedID = "", bool distinct = false)
        {
            List<SelectListItem> list0 = new List<SelectListItem>();
            T _t = (T)Activator.CreateInstance(typeof(T));
            if (showAll)
            {
                SelectListItem item = new SelectListItem();
                item.Text = "=签收人员=";
                item.Value = "-1";
                list0.Add(item);
            }
            PropertyInfo[] propertys = _t.GetType().GetProperties();
            foreach (T item in list)
            {
                SelectListItem item0 = new SelectListItem();
                int i = 0;
                foreach (PropertyInfo pi in propertys)
                {
                    if (pi.Name == valueField)
                    {
                        i++;
                        item0.Value = Convert.ToString(pi.GetValue(item, null) ?? DBNull.Value);
                        if (item0.Value == selectedID)
                        {
                            item0.Selected = true;
                        }
                    }
                    if (pi.Name == textField)
                    {
                        if (pi.Name != valueField)
                        {
                            i++;
                        }
                        item0.Text = Convert.ToString(pi.GetValue(item, null) ?? DBNull.Value);
                    }
                    if (i == 2)
                    {
                        break;
                    }
                }
                if (distinct && list0.FindIndex(x => x.Value == item0.Value) >= 0)
                {
                    continue;
                }
                list0.Add(item0);
            }
            return list0;
        }

        /// <summary>
        /// DataSet转换为SelectListItem泛型集合
        /// </summary>
        /// <param name="ds">DataSet数据集</param>
        /// <param name="valueField">值字段</param>
        /// <param name="textField">显示字段</param>
        /// <param name="showAll">显示选择所有</param>
        /// <param name="selectedID">选中值</param>
        /// <param name="selectNull">未选中时显示</param>
        /// <returns>返回SelectListItem泛型集合</returns>
        public static List<SelectListItem> ToSelectList<T>(this IList<T> list, string valueField, string textField, bool showAll = false, string selectedID = "", string selectNull = "==请选择==")
        {
            List<SelectListItem> list0 = new List<SelectListItem>();
            T _t = (T)Activator.CreateInstance(typeof(T));
            if (showAll)
            {
                SelectListItem item = new SelectListItem();
                item.Text = selectNull;
                item.Value = "-1";
                list0.Add(item);
            }
            PropertyInfo[] propertys = _t.GetType().GetProperties();
            foreach (T item in list)
            {
                SelectListItem item0 = new SelectListItem();
                int i = 0;
                foreach (PropertyInfo pi in propertys)
                {
                    if (pi.Name == valueField)
                    {
                        i++;
                        item0.Value = Convert.ToString(pi.GetValue(item, null) ?? DBNull.Value);
                        if (item0.Value == selectedID)
                        {
                            item0.Selected = true;
                        }
                    }
                    else if (pi.Name == textField)
                    {
                        i++;
                        item0.Text = Convert.ToString(pi.GetValue(item, null) ?? DBNull.Value);
                    }
                    if (i == 2)
                    {
                        break;
                    }
                }

                list0.Add(item0);
            }
            return list0;
        }
    }
}
