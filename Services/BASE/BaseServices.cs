using Common.Model;
using IServices;
using IServices.BASE;
using Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.BASE
{
    /// <summary>
    /// 基类服务实现类
    /// </summary>
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        //通过在子类的构造函数中注入，这里是基类，不用构造函数
        public ISugarRepository<TEntity> BaseDal { get; set; }

        /// <summary>
        /// 根据ID查询详情
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public TEntity QueryById(object objId)
        {
            return BaseDal.QueryById(objId);
        }

        /// <summary>
        /// 根据ID查询详情
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否缓存</param>
        /// <returns></returns>
        public TEntity QueryById(object objId, bool blnUseCache = false)
        {
            return BaseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 根据ID查询详情(异步)
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<TEntity> T_QueryById(object objId)
        {
            return await BaseDal.T_QueryById(objId);
        }

        /// <summary>
        /// 根据ID查询详情(异步)
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否缓存</param>
        /// <returns></returns>
        public async Task<TEntity> T_QueryById(object objId, bool blnUseCache = false)
        {
            return await BaseDal.T_QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 根据ID集合查询列表集合
        /// </summary>
        /// <param name="lstIds">ID数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public List<TEntity> QueryByIDs(object[] lstIds)
        {
            return BaseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 根据ID集合查询列表集合(异步)
        /// </summary>
        /// <param name="lstIds">ID数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_QueryByIDs(object[] lstIds)
        {
            return await BaseDal.T_QueryByIDs(lstIds);
        }

        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Add(TEntity model)
        {
            return BaseDal.Add(model);
        }

        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="model">实体列表</param>
        /// <returns></returns>
        public int Add(List<TEntity> modelList)
        {
            return BaseDal.Add(modelList);
        }

        /// <summary>
        /// 新增实体信息(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<int> T_Add(TEntity model)
        {
            return await BaseDal.T_Add(model);
        }

        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            return BaseDal.DeleteById(id);
        }

        /// <summary>
        /// 根据ID删除实体(异步)
        /// </summary>
        /// <param name="id">必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<bool> T_DeleteById(object id)
        {
            return await BaseDal.T_DeleteById(id);
        }

        /// <summary>
        /// 根据实体信息删除实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public bool Delete(TEntity model)
        {
            return BaseDal.Delete(model);
        }

        /// <summary>
        /// 根据实体信息删除实体(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> T_Delete(TEntity model)
        {
            return await BaseDal.T_Delete(model);
        }

        /// <summary>
        /// 根据ID数组删除多个实体信息
        /// </summary>
        /// <param name="ids">主键ID</param>
        public bool DeleteByIds(object[] ids)
        {
            return BaseDal.DeleteByIds(ids);
        }

        /// <summary>
        /// 根据ID数组删除多个实体信息(异步)
        /// </summary>
        /// <param name="ids">主键ID数组</param>
        /// <returns></returns>
        public async Task<bool> T_DeleteByIds(object[] ids)
        {
            return await BaseDal.T_DeleteByIds(ids);
        }

        /// <summary>
        /// 根据ID删除实体信息，返回受影响的行数
        /// </summary>
        /// <param name="ids">id数组</param>
        /// <returns></returns>
        public int DeleteByIdsEx(object[] ids)
        {
            return BaseDal.DeleteByIdsEx(ids);
        }

        /// <summary>
        /// 更新实体信息(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public bool Update(TEntity model)
        {
            return BaseDal.Update(model);
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Update(List<TEntity> modelList)
        {
            return BaseDal.Update(modelList);
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> T_Update(TEntity model)
        {
            return await BaseDal.T_Update(model);
        }

        /// <summary>
        /// 根据条件更新实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public bool Update(TEntity entity, string strWhere)
        {
            return BaseDal.Update(entity, strWhere);
        }

        /// <summary>
        /// 根据条件更新实体信息(异步)
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<bool> T_Update(TEntity entity, string strWhere)
        {
            return await BaseDal.T_Update(entity, strWhere);
        }

        /// <summary>
        /// 根据条件更新实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">实体列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public bool Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return BaseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }

        /// <summary>
        /// 根据条件更新实体信息(返回受影响行数)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">实体列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int UpdateEx(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return BaseDal.UpdateEx(entity, lstColumns, lstIgnoreColumns, strWhere);
        }

        /// <summary>
        /// 根据条件更新实体信息(异步)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">实体列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<bool> T_Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return await BaseDal.T_Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }

        /// <summary>
        /// 查询所有实体列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> Query()
        {
            return BaseDal.Query();
        }

        /// <summary>
        /// 查询所有结果返回DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDT()
        {
            return BaseDal.QueryDT();
        }

        /// <summary>
        /// 查询所有实体信息(异步)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query()
        {
            return await BaseDal.T_Query();
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public List<TEntity> Query(string strWhere)
        {
            return BaseDal.Query(strWhere);
        }

        /// <summary>
        /// 查询所有结果返回DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable QueryDT(string strWhere)
        {
            return BaseDal.QueryDT(strWhere);
        }

        /// <summary>
        /// 根据条件查询实体列表(异步)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(string strWhere)
        {
            return await BaseDal.T_Query(strWhere);
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return BaseDal.Query(whereExpression);
        }

        /// <summary>
        /// 根据条件查询DataTable
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression)
        {
            return BaseDal.QueryEx(whereExpression);
        }

        /// <summary>
        /// 根据条件查询实体列表(异步)
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.T_Query(whereExpression);
        }

        /// <summary>
        /// 根据条件查询，根据指定字段排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return BaseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询，根据指定字段排序DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return BaseDal.QueryEx(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询，根据指定字段排序（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await BaseDal.T_Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询，根据指定条件排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return BaseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        /// <summary>
        /// 根据条件查询，根据指定条件排序DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return BaseDal.QueryEx(whereExpression, orderByExpression, isAsc);
        }

        /// <summary>
        /// 根据条件查询，根据指定条件排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await BaseDal.T_Query(whereExpression, orderByExpression, isAsc);
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        public List<TEntity> Query(string strWhere, string strOrderByFileds)
        {
            return BaseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询Datatable
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, string strOrderByFileds)
        {
            return BaseDal.QueryEx(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询列表(异步)
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(string strWhere, string strOrderByFileds)
        {
            return await BaseDal.T_Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询数据列表
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return BaseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return BaseDal.QueryEx(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询数据列表（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await BaseDal.T_Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询数据列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public List<TEntity> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return BaseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, int intTop, string strOrderByFileds)
        {
            return BaseDal.QueryEx(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询数据列表（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await BaseDal.T_Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return BaseDal.Query(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return BaseDal.QueryEx(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据列表(异步)
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await BaseDal.T_Query(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return BaseDal.Query(strWhere, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public DataTable QueryEx(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return BaseDal.QueryEx(strWhere, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, ref Pagination paging)
        {
            return BaseDal.QueryEx(strWhere, ref paging);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, string strOrderByFileds, ref Pagination paging)
        {
            return BaseDal.QueryEx(strWhere, strOrderByFileds, ref paging);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable QueryEx(ref Pagination paging, params SugarParameter[] parameters)
        {
            return BaseDal.QueryEx(ref paging, parameters);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, ref Pagination paging, params SugarParameter[] parameters)
        {
            return BaseDal.QueryEx(strWhere, ref paging, parameters);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, string strOrderByFileds, ref Pagination paging, params SugarParameter[] parameters)
        {
            return BaseDal.QueryEx(strWhere, strOrderByFileds, ref paging, parameters);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, string strOrderByFileds, params SugarParameter[] parameters)
        {
            return BaseDal.QueryEx(strWhere, strOrderByFileds, parameters);
        }

        /// <summary>
        /// 分页查询数据列表(异步)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> T_Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await BaseDal.T_Query(strWhere, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public PageModel<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return BaseDal.QueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }


        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public PageModel<DataTable> QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return BaseDal.QueryPageEx(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, ref Pagination paging)
        {
            return BaseDal.QueryPageEx(whereExpression, ref paging);
        }

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="paging"></param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, ref Pagination paging, string strOrderByFileds = null)
        {
            return BaseDal.QueryPageEx(whereExpression, ref paging, strOrderByFileds);
        }

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds = null, params SugarParameter[] parameters)
        {
            return BaseDal.QueryPageEx(whereExpression, strOrderByFileds, parameters);
        }

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="where">数据</param>
        /// <param name="orderBy">排序</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public DataTable QueryPageEx(ref Pagination paging, string where = "", string orderBy = "")
        {
            return BaseDal.QueryPageEx(ref paging, where, orderBy);
        }

        /// <summary>
        /// 分页查询(异步)
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> T_QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.T_QueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql)
        {
            return BaseDal.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, ref Pagination paging)
        {
            return BaseDal.ExecuteDataTable(sql, ref paging);
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, params SugarParameter[] parameters)
        {
            return BaseDal.ExecuteDataTable(sql, parameters);
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, ref Pagination paging, params SugarParameter[] parameters)
        {
            return BaseDal.ExecuteDataTable(sql, ref paging, parameters);
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql)
        {
            return BaseDal.ExecuteList(sql);
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql, ref Pagination paging)
        {
            return BaseDal.ExecuteList(sql, ref paging);
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql, params SugarParameter[] parameters)
        {
            return BaseDal.ExecuteList(sql, parameters);
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging">命令类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql, ref Pagination paging, params SugarParameter[] parameters)
        {
            return BaseDal.ExecuteList(sql, ref paging, parameters);
        }

        /// <summary>
        /// 执行返回首行首字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, params SugarParameter[] parameters)
        {
            return BaseDal.ExecuteScalar(sql, parameters);
        }

        /// <summary>
        /// 执行返回首行首字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            return BaseDal.ExecuteScalar(sql);
        }
    }
}
