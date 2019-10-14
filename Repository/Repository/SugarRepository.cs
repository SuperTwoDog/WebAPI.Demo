using Common;
using Common.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// sqlsugar仓储类
    /// </summary>
    public class SugarRepository<TEntity> : ISugarRepository<TEntity> where TEntity : class, new()
    {
        private SugarDbContext _context;
        private SqlSugarClient _db;
        private SimpleClient<TEntity> _entityDb;

        public SugarDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }
        internal SimpleClient<TEntity> entityDb
        {
            get { return _entityDb; }
            private set { _entityDb = value; }
        }

        public SugarRepository()
        {
            SugarDbContext.Init(DbProviderFactoryHelper.GetConnectionString(), (SqlSugar.DbType)DbProviderFactoryHelper.GetDBType());
            _context = SugarDbContext.GetDbContext();
            _db = _context.Db;
            _entityDb = _context.GetEntityDB<TEntity>(_db);
        }

        /// <summary>
        /// 根据ID查询详情
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public TEntity QueryById(object objId)
        {
            return _db.Queryable<TEntity>().In(objId).Single();
        }

        /// <summary>
        /// 根据ID查询详情
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否缓存</param>
        /// <returns></returns>
        public TEntity QueryById(object objId, bool blnUseCache = false)
        {
            return _db.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).Single();
        }

        /// <summary>
        /// 根据ID查询详情(异步)
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<TEntity> T_QueryById(object objId)
        {
            return await _db.Queryable<TEntity>().In(objId).SingleAsync();
        }

        /// <summary>
        /// 根据ID查询详情(异步)
        /// </summary>
        /// <param name="objId">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否缓存</param>
        /// <returns></returns>
        public async Task<TEntity> T_QueryById(object objId, bool blnUseCache = false)
        {
            return await _db.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).SingleAsync();
        }

        /// <summary>
        /// 根据ID集合查询列表集合
        /// </summary>
        /// <param name="lstIds">ID数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public List<TEntity> QueryByIDs(object[] lstIds)
        {
            return _db.Queryable<TEntity>().In(lstIds).ToList();
        }

        /// <summary>
        /// 根据ID集合查询列表集合(异步)
        /// </summary>
        /// <param name="lstIds">ID数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_QueryByIDs(object[] lstIds)
        {
            return await _db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Add(TEntity model)
        {
            int result = 0;
            try
            {
                _db.Ado.BeginTran();
                var insert = _db.Insertable(model);
                result = insert.ExecuteCommand();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="model">实体列表</param>
        /// <returns></returns>
        public int Add(List<TEntity> modelList)
        {
            int result = 0;
            try
            {
                _db.Ado.BeginTran();
                var insert = _db.Insertable(modelList);
                result = insert.ExecuteCommand();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 新增实体信息(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<int> T_Add(TEntity model)
        {
            int result = 0;
            try
            {
                _db.Ado.BeginTran();
                var insert = _db.Insertable(model);
                result = await insert.ExecuteCommandAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">ID（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Deleteable<TEntity>(id).ExecuteCommandHasChange();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据ID删除实体(异步)
        /// </summary>
        /// <param name="id">必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns></returns>
        public async Task<bool> T_DeleteById(object id)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据实体信息删除实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public bool Delete(TEntity model)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Deleteable(model).ExecuteCommandHasChange();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据实体信息删除实体(异步)
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> T_Delete(TEntity model)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = await _db.Deleteable<TEntity>(model).ExecuteCommandHasChangeAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据ID数组删除多个实体信息
        /// </summary>
        /// <param name="ids">主键ID</param>
        public bool DeleteByIds(object[] ids)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChange();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据ID数组删除多个实体信息(异步)
        /// </summary>
        /// <param name="ids">主键ID数组</param>
        /// <returns></returns>
        public async Task<bool> T_DeleteByIds(object[] ids)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = await _db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据ID删除实体信息，返回受影响的行数
        /// </summary>
        /// <param name="ids">id数组</param>
        /// <returns></returns>
        public int DeleteByIdsEx(object[] ids)
        {
            int result = 0;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Deleteable<TEntity>().In(ids).ExecuteCommand();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public bool Update(TEntity model)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Updateable(model).ExecuteCommandHasChange();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Update(List<TEntity> modelList)
        {
            int result = 0;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Updateable(modelList).ExecuteCommand();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> T_Update(TEntity model)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = await _db.Updateable(model).ExecuteCommandHasChangeAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据条件更新实体信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public bool Update(TEntity entity, string strWhere)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Updateable(entity).Where(strWhere).ExecuteCommandHasChange();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 根据条件更新实体信息(异步)
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<bool> T_Update(TEntity entity, string strWhere)
        {
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                result = await _db.Updateable(entity).Where(strWhere).ExecuteCommandHasChangeAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
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
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                IUpdateable<TEntity> up = _db.Updateable(entity);
                if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                {
                    up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
                }
                if (lstColumns != null && lstColumns.Count > 0)
                {
                    up = up.UpdateColumns(lstColumns.ToArray());
                }
                if (!string.IsNullOrEmpty(strWhere))
                {
                    up = up.Where(strWhere);
                }
                result = up.ExecuteCommandHasChange();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
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
            int result = 0;
            try
            {
                _db.Ado.BeginTran();
                IUpdateable<TEntity> up = _db.Updateable(entity);
                if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                {
                    up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
                }
                if (lstColumns != null && lstColumns.Count > 0)
                {
                    up = up.UpdateColumns(lstColumns.ToArray());
                }
                if (!string.IsNullOrEmpty(strWhere))
                {
                    up = up.Where(strWhere);
                }
                result = up.ExecuteCommand();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
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
            bool result = false;
            try
            {
                _db.Ado.BeginTran();
                IUpdateable<TEntity> up = _db.Updateable(entity);
                if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                {
                    up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
                }
                if (lstColumns != null && lstColumns.Count > 0)
                {
                    up = up.UpdateColumns(lstColumns.ToArray());
                }
                if (!string.IsNullOrEmpty(strWhere))
                {
                    up = up.Where(strWhere);
                }
                result = await up.ExecuteCommandHasChangeAsync();
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 查询所有实体列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> Query()
        {
            return _db.Queryable<TEntity>().ToList();
        }

        /// <summary>
        /// 查询所有结果返回DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDT()
        {
            return _db.Queryable<TEntity>().ToDataTable();
        }

        /// <summary>
        /// 查询所有实体信息(异步)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public List<TEntity> Query(string strWhere)
        {
            return _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList();
        }

        /// <summary>
        /// 查询所有结果返回DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable QueryDT(string strWhere)
        {
            return _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTable();
        }

        /// <summary>
        /// 根据条件查询实体列表(异步)
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(string strWhere)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToList();
        }

        /// <summary>
        /// 根据条件查询DataTable
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToDataTable();
        }

        /// <summary>
        /// 根据条件查询实体列表(异步)
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 根据条件查询，根据指定字段排序
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(strOrderByFileds != null, strOrderByFileds).ToList();
        }

        /// <summary>
        /// 根据条件查询，根据指定字段排序DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryEx(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(strOrderByFileds != null, strOrderByFileds).ToDataTable();
        }

        /// <summary>
        /// 根据条件查询，根据指定字段排序（异步）
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：如name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(strOrderByFileds != null, strOrderByFileds).ToListAsync();
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
            return _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList();
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
            return _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToDataTable();
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
            return await _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        public List<TEntity> Query(string strWhere, string strOrderByFileds)
        {
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList();
        }

        /// <summary>
        /// 根据条件查询Datatable
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, string strOrderByFileds)
        {
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTable();
        }

        /// <summary>
        /// 根据条件查询列表(异步)
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderByFileds">排序字段：name asc,age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> T_Query(string strWhere, string strOrderByFileds)
        {
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList();
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToDataTable();
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
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList();
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToDataTable();
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
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToListAsync();
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize);
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToDataTablePage(intPageIndex, intPageSize);
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
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(intPageIndex, intPageSize);
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize);
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTablePage(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public DataTable QueryEx(string strWhere, ref Pagination paging)
        {
            int totalCount = 0;
            DataTable dt = _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTablePage(paging.PageIndex, paging.PageRows, ref totalCount);
            paging.RecordCount = totalCount;
            return dt;
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
            int totalCount = 0;
            DataTable dt = _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTablePage(paging.PageIndex, paging.PageRows);
            paging.RecordCount = totalCount;
            return dt;
        }

        /// <summary>
        /// 分页查询DataTable
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public DataTable QueryEx(ref Pagination paging, params SugarParameter[] parameters)
        {
            int totalCount = 0;
            DataTable dt = _db.Queryable<TEntity>().ToDataTablePage(paging.PageIndex, paging.PageRows);
            paging.RecordCount = totalCount;
            return dt;
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
            int totalCount = 0;
            DataTable dt = _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTablePage(paging.PageIndex, paging.PageRows);
            paging.RecordCount = totalCount;
            return dt;
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
            int totalCount = 0;
            DataTable dt = _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).AddParameters(parameters).ToDataTablePage(paging.PageIndex, paging.PageRows);
            paging.RecordCount = totalCount;
            return dt;
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToDataTable();
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
            return await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageListAsync(intPageIndex, intPageSize);
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
            int totalCount = 0;
            var list = _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize, ref totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<TEntity>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
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
            int totalCount = 0;
            var list = _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToDataTablePage(intPageIndex, intPageSize, ref totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<DataTable>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, dataDT = list };
        }

        /// <summary>
        /// 分页查询数据DataTable
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public DataTable QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, ref Pagination paging)
        {
            int totalCount = 0;
            var dt = _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToDataTablePage(paging.PageIndex, paging.PageRows, ref totalCount);
            paging.RecordCount = totalCount;
            return dt;
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
            int totalCount = 0;
            var dt = _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToDataTablePage(paging.PageIndex, paging.PageRows, ref totalCount);
            paging.RecordCount = totalCount;
            return dt;
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
            return _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToDataTable();
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
            int totalCount = 0;
            var dt = _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy).WhereIF(!string.IsNullOrEmpty(where), where).ToDataTablePage(paging.PageIndex, paging.PageRows, ref totalCount);
            paging.RecordCount = totalCount;
            return dt;
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
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<TEntity>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql)
        {
            return _db.SqlQueryable<DataTable>(sql).ToDataTable();
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, ref Pagination paging)
        {
            int totalCount = 0;
            DataTable dt = _db.SqlQueryable<DataTable>(sql).ToDataTablePage(paging.PageIndex, paging.PageRows, ref totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / paging.PageRows.ObjToDecimal())).ObjToInt();
            paging.RecordCount = totalCount;
            return dt;
        }

        /// <summary>
        /// 执行返回数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, params SugarParameter[] parameters)
        {
            return _db.SqlQueryable<DataTable>(sql).AddParameters(parameters).ToDataTable();
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
            int totalCount = 0;
            DataTable dt = _db.SqlQueryable<DataTable>(sql).AddParameters(parameters).ToDataTablePage(paging.PageIndex, paging.PageRows, ref totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / paging.PageRows.ObjToDecimal())).ObjToInt();
            paging.RecordCount = totalCount;
            return dt;
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql)
        {
            return _db.SqlQueryable<TEntity>(sql).ToList();
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql, ref Pagination paging)
        {
            int totalCount = 0;
            var list = _db.SqlQueryable<TEntity>(sql).ToPageList(paging.PageIndex, paging.PageRows, ref totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / paging.PageRows.ObjToDecimal())).ObjToInt();
            paging.RecordCount = totalCount;
            return list;
        }

        /// <summary>
        /// 执行返回数据列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteList(string sql, params SugarParameter[] parameters)
        {
            return _db.SqlQueryable<TEntity>(sql).AddParameters(parameters).ToList();
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
            int totalCount = 0;
            var list = _db.SqlQueryable<TEntity>(sql).AddParameters(parameters).ToPageList(paging.PageIndex, paging.PageRows, ref totalCount);
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / paging.PageRows.ObjToDecimal())).ObjToInt();
            paging.RecordCount = totalCount;
            return list;
        }

        /// <summary>
        /// 执行返回首行首字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, params SugarParameter[] parameters)
        {
            object result = null;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Ado.GetScalar(sql, parameters);
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }

        /// <summary>
        /// 执行返回首行首字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            object result = null;
            try
            {
                _db.Ado.BeginTran();
                result = _db.Ado.GetScalar(sql);
                _db.Ado.CommitTran();
            }
            catch (Exception)
            {
                _db.Ado.RollbackTran();
            }
            return result;
        }
    }
}
