using Common.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IServices.BASE
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseServices<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据ID查询详情
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        TEntity QueryById(object objId);

        /// <summary>
        /// 根据ID查询详情
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否缓存</param>
        /// <returns></returns>
        TEntity QueryById(object objId, bool blnUseCache = false);

        /// <summary>
        /// 根据ID查询详情(异步)
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        Task<TEntity> T_QueryById(object objId);

        /// <summary>
        /// 根据ID查询详情(异步)
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否缓存</param>
        /// <returns></returns>
        Task<TEntity> T_QueryById(object objId, bool blnUseCache = false);

        /// <summary>
        /// 根据ID集合查询列表集合
        /// </summary>
        /// <param name="lstIds">ID数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        List<TEntity> QueryByIDs(object[] lstIds);

        /// <summary>
        /// 根据ID集合查询列表集合(异步)
        /// </summary>
        /// <param name="lstIds">ID数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        Task<List<TEntity>> T_QueryByIDs(object[] lstIds);

        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        int Add(TEntity model);

        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="model">实体列表</param>
        /// <returns></returns>
        int Add(List<TEntity> modelList);

        /// <summary>
        /// 新增实体信息(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        Task<int> T_Add(TEntity model);

        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        bool DeleteById(object id);

        /// <summary>
        /// 根据ID删除实体(异步)
        /// </summary>
        /// <param name="id">必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        Task<bool> T_DeleteById(object id);

        /// <summary>
        /// 根据实体信息删除实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        bool Delete(TEntity model);

        /// <summary>
        /// 根据实体信息删除实体(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        Task<bool> T_Delete(TEntity model);

        /// <summary>
        /// 根据ID数组删除多个实体信息
        /// </summary>
        /// <param name="ids">主键ID</param>
        bool DeleteByIds(object[] ids);

        /// <summary>
        /// 根据ID数组删除多个实体信息(异步)
        /// </summary>
        /// <param name="ids">主键ID数组</param>
        /// <returns></returns>
        Task<bool> T_DeleteByIds(object[] ids);

        /// <summary>
        /// 根据ID删除实体信息，返回受影响的行数
        /// </summary>
        /// <param name="ids">id数组</param>
        /// <returns></returns>
        int DeleteByIdsEx(object[] ids);

        /// <summary>
        /// 更新实体信息(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        bool Update(TEntity model);

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        int Update(List<TEntity> modelList);

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        Task<bool> T_Update(TEntity model);

        /// <summary>
        /// 根据条件更新实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        bool Update(TEntity entity, string strWhere);

        /// <summary>
        /// 根据条件更新实体信息(异步)
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        Task<bool> T_Update(TEntity entity, string strWhere);

        /// <summary>
        /// 根据条件更新实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">实体列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        bool Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        /// <summary>
        /// 根据条件更新实体信息(返回受影响行数)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">实体列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        int UpdateEx(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        /// <summary>
        /// 根据条件更新实体信息(异步)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">实体列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        Task<bool> T_Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        /// <summary>
        /// 查询所有实体列表
        /// </summary>
        /// <returns></returns>
        List<TEntity> Query();

        /// <summary>
        /// 查询所有结果返回DataTable
        /// </summary>
        /// <returns></returns>
        DataTable QueryDT();

        /// <summary>
        /// 查询所有实体信息(异步)
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> T_Query();

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        List<TEntity> Query(string strWhere);

        /// <summary>
        /// 查询所有结果返回DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        DataTable QueryDT(string strWhere);

        /// <summary>
        /// 根据条件查询实体列表(异步)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(string strWhere);

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件查询DataTable
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件查询实体列表(异步)
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件查询，根据指定字段排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询，根据指定字段排序DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询，根据指定字段排序（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询，根据指定条件排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 根据条件查询，根据指定条件排序DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 根据条件查询，根据指定条件排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        List<TEntity> Query(string strWhere, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询Datatable
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询列表(异步)
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(string strWhere, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询数据列表
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询数据列表（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询数据列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        List<TEntity> Query(string strWhere, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件查询数据列表（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        Task<List<TEntity>> T_Query(string strWhere, int intTop, string strOrderByFileds);

        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 分页查询数据列表(异步)
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        DataTable QueryEx(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, ref Pagination paging);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, string strOrderByFileds, ref Pagination paging);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, string strOrderByFileds, params SugarParameter[] parameters);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable QueryEx(ref Pagination paging, params SugarParameter[] parameters);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, ref Pagination paging, params SugarParameter[] parameters);

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable QueryEx(string strWhere, string strOrderByFileds, ref Pagination paging, params SugarParameter[] parameters);

        /// <summary>
        /// 分页查询数据列表(异步)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> T_Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 分页查询数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        PageModel<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);


        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        PageModel<DataTable> QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, ref Pagination paging);

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="paging"></param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, ref Pagination paging, string strOrderByFileds = null);

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds = null, params SugarParameter[] parameters);

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="where">数据</param>
        /// <param name="orderBy">排序</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        DataTable QueryPageEx(ref Pagination paging, string where = "", string orderBy = "");

        /// <summary>
        /// 分页查询(异步)
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        Task<PageModel<TEntity>> T_QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql);

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql, ref Pagination paging);

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging">命令类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql, ref Pagination paging, params SugarParameter[] parameters);

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        List<TEntity> ExecuteList(string sql);

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        List<TEntity> ExecuteList(string sql, ref Pagination paging);

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        List<TEntity> ExecuteList(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging">命令类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        List<TEntity> ExecuteList(string sql, ref Pagination paging, params SugarParameter[] parameters);

        /// <summary>
        /// 执行返回首行首字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        object ExecuteScalar(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 执行返回首行首字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        object ExecuteScalar(string sql);
    }
}