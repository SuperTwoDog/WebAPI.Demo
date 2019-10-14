using Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common
{
    public static partial class Extention
    {
        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> list = new List<T>();

            //确认参数有效,若无效则返回Null
            if (dt == null)
                return list;
            else if (dt.Rows.Count == 0)
                return list;

            Dictionary<string, string> dicField = new Dictionary<string, string>();
            Dictionary<string, string> dicProperty = new Dictionary<string, string>();
            Type type = typeof(T);

            //创建字段字典，方便查找字段名
            type.GetFields().ForEach(aFiled =>
            {
                dicField.Add(aFiled.Name.ToLower(), aFiled.Name);
            });

            //创建属性字典，方便查找属性名
            type.GetProperties().ForEach(aProperty =>
            {
                dicProperty.Add(aProperty.Name.ToLower(), aProperty.Name);
            });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string memberKey = dt.Columns[j].ColumnName.ToLower();

                    //字段赋值
                    if (dicField.ContainsKey(memberKey))
                    {
                        FieldInfo theField = type.GetField(dicField[memberKey]);
                        var dbValue = dt.Rows[i][j];
                        if (dbValue.GetType() == typeof(DBNull))
                            dbValue = null;
                        if (dbValue != null)
                        {
                            Type memberType = theField.FieldType;
                            if (memberType.IsGenericType && memberType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                NullableConverter newNullableConverter = new NullableConverter(memberType);
                                dbValue = newNullableConverter.ConvertFrom(dbValue);
                            }
                            else
                            {
                                dbValue = Convert.ChangeType(dbValue, memberType);
                            }
                        }
                        theField.SetValue(_t, dbValue);
                    }
                    //属性赋值
                    if (dicProperty.ContainsKey(memberKey))
                    {
                        PropertyInfo theProperty = type.GetProperty(dicProperty[memberKey]);
                        var dbValue = dt.Rows[i][j];
                        if (dbValue.GetType() == typeof(DBNull))
                            dbValue = null;
                        if (dbValue != null)
                        {
                            Type memberType = theProperty.PropertyType;
                            if (memberType.IsGenericType && memberType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                NullableConverter newNullableConverter = new NullableConverter(memberType);
                                dbValue = newNullableConverter.ConvertFrom(dbValue);
                            }
                            else
                            {
                                dbValue = Convert.ChangeType(dbValue, memberType);
                            }
                        }
                        theProperty.SetValue(_t, dbValue);
                    }
                }
                list.Add(_t);
            }
            return list;
        }

        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public static List<T> EmitToList<T>(this DataTable dt)
        {
            //确认参数有效
            if (dt == null)
                return null;

            List<T> list = new List<T>();
            var objBuilder = EmitHelper.CreateBuilder(typeof(T));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = (T)objBuilder();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }

        /// <summary>
        ///将DataTable转换为标准的CSV字符串
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>返回标准的CSV</returns>
        public static string ToCsvStr(this DataTable dt)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colum = dt.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// DataTable转换为SelectListItem泛型集合
        /// </summary>
        /// <param name="ds">DataSet数据集</param>
        /// <param name="valueField">值字段</param>
        /// <param name="textField">显示字段</param>
        /// <param name="selectedID">选中值</param>
        /// <returns>返回SelectListItem泛型集合</returns>
        public static List<SelectListItem> ToSelectList(this DataTable dt, string valueField, string textField, string selectedID = "")
        {
            return dt.ToSelectList(valueField, textField, false, selectedID);
        }

        /// <summary>
        /// DataSet转换为SelectListItem泛型集合
        /// </summary>
        /// <param name="ds">DataSet数据集</param>
        /// <param name="valueField">值字段</param>
        /// <param name="textField">显示字段</param>
        /// <param name="showAll">显示选择所有</param>
        /// <param name="selectedID">选中值</param>
        /// <param name="allText">showAll时显示的内容</param>
        /// <param name="allValue">showAll时显示的内容值</param>
        /// <returns>返回SelectListItem泛型集合</returns>
        public static List<SelectListItem> ToSelectList(this DataTable dt, string valueField, string textField, bool showAll, string selectedID = "", string allText = "", string allValue = "-1")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (showAll)
            {
                SelectListItem item = new SelectListItem();
                item.Text = string.IsNullOrEmpty(allText) ? "==请选择==" : allText;
                item.Value = allValue;
                list.Add(item);
            }
            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Convert.ToString(dr[textField]);
                item.Value = Convert.ToString(dr[valueField]);
                item.Selected = Convert.ToString(dr[valueField]) == selectedID;
                list.Add(item);
            }
            return list;
        }
    }
}
