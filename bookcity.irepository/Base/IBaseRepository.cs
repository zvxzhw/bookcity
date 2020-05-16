using bookcity.entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookcity.irepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity:class
    {
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);

        Task<TEntity> QueryById(object objId);

        Task<List<TEntity>> QueryByIds(object[] lstIds);

        Task<int> Add(TEntity model);

        Task<bool> Delete(TEntity entity);

        Task<bool> DeleteById(object id);

        Task<bool> DeleteByIds(object[] ids);

        Task<bool> Delete(Expression<Func<TEntity,bool>> expression);

        Task<bool> Update(TEntity model);

        Task<bool> Update(TEntity entity,string strWhere);

        Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        // 存储过程IBaseRepository
        Task<object> StoredProcedure(string sql, object parameters);
        //DataSet StoredSqlServerProcedure(string procedureName, params IDataParameter[] commandParameters);
        Task<List<TEntity>> Query();
        Task<List<TEntity>> Query(string strWhere);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);

        Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);


        Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);

        Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();
    }

}
