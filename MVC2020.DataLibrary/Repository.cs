using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.DataLibrary
{
    public class Repository<T> where T : class
    {
        //数据库连接字符串
        public DbContext DbContext { get; set; }

        public Repository( ) { }

        public Repository(DbContext dbcontext) { DbContext = dbcontext; }

        //////////////////////////////////

        #region 查

        #region 查单实体 （重载两次）（根据ID和lamdba）

        //查找实体-返回唯一实体
        public T Find(int ID) { return DbContext.Set<T>().Find(ID); }

        //查找实体-返回唯一实体 ？
        public T Find(Expression<Func<T,bool>> where) { return DbContext.Set<T>().SingleOrDefault(where); }

        #endregion

        #region 查多实体 （重载七次）

        //查找实体列表-默认表所有条数
        public IQueryable<T> FindList( )
        {
            return DbContext.Set<T>();
        }

        //查找实体列表-拉姆表达式-默认表所有条数
        public IQueryable<T> FindList(Expression<Func<T,bool>> where)
        {
            return DbContext.Set<T>().Where(where);
        }

        /// 查找实体列表-拉姆表达式（返回数量可选）
        public IQueryable<T> FindList(Expression<Func<T,bool>> where,int number)
        {
            return DbContext.Set<T>().Where(where).Take(number);
        }

        /// <summary>
        /// 查找实体列表-排序（正反）
        /// </summary>
        /// <typeparam name="TKey">排序建类型</typeparam>
        /// <param name="order">排序表达式</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        public IQueryable<T> FindList<TKey>(Expression<Func<T,TKey>> order,bool asc)
        {
            return asc ? DbContext.Set<T>().OrderBy(order) : DbContext.Set<T>().OrderByDescending(order);
        }

        /// <summary>
        /// 查找实体列表-（排序）（正反）（返回数量）
        /// </summary>
        /// <typeparam name="TKey">排序键类型</typeparam>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        /// <param name="number">获取的记录数量</param>
        /// <returns></returns>
        public IQueryable<T> FindList<TKey>(Expression<Func<T,TKey>> order,bool asc,int number)
        {
            return asc ? DbContext.Set<T>().OrderBy(order).Take(number) : DbContext.Set<T>().OrderByDescending(order).Take(number);
        }


        /// <summary>
        /// 查找实体列表-Lambda（排序）（正反）
        /// </summary>
        /// <typeparam name="TKey">排序键类型</typeparam>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        public IQueryable<T> FindList<TKey>(Expression<Func<T,bool>> where,Expression<Func<T,TKey>> order,bool asc)
        {
            return asc ? DbContext.Set<T>().Where(where).OrderBy(order) : DbContext.Set<T>().Where(where).OrderByDescending(order);
        }

        /// <summary>
        /// 查找实体列表-Lambda（排序）（正反）（返回数量）
        /// </summary>
        /// <typeparam name="TKey">排序键类型</typeparam>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        /// <param name="number">获取的记录数量</param>
        /// <returns></returns>
        public IQueryable<T> FindList<TKey>(Expression<Func<T,bool>> where,Expression<Func<T,TKey>> order,bool asc,int number)
        {
            return asc ? DbContext.Set<T>().Where(where).OrderBy(order).Take(number) : DbContext.Set<T>().Where(where).OrderByDescending(order).Take(number);
        }


        #endregion

        #region 查分页

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList(int pageSize,int pageIndex,out int totalNumber)
        {
            if(pageIndex < 1) pageIndex = 1;
            if(pageSize < 1) pageSize = 10;
            IQueryable<T> _list = DbContext.Set<T>();
            totalNumber = _list.Count();
            return _list.Skip((pageIndex - 1) * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList<TKey>(int pageSize,int pageIndex,out int totalNumber,Expression<Func<T,TKey>> order,bool asc)
        {
            if(pageIndex < 1) pageIndex = 1;
            if(pageSize < 1) pageSize = 10;
            IQueryable<T> _list = DbContext.Set<T>();
            _list = asc ? _list.OrderBy(order) : _list.OrderByDescending(order);
            totalNumber = _list.Count();
            return _list.Skip((pageIndex - 1) * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="where">查询表达式</param>
        public IQueryable<T> FindPageList(int pageSize,int pageIndex,out int totalNumber,Expression<Func<T,bool>> where)
        {
            if(pageIndex < 1) pageIndex = 1;
            if(pageSize < 1) pageSize = 10;
            IQueryable<T> _list = DbContext.Set<T>().Where(where);
            totalNumber = _list.Count();
            return _list.Skip((pageIndex - 1) * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="where">查询表达式</param>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        public IQueryable<T> FindPageList<TKey>(int pageSize,int pageIndex,out int totalNumber,Expression<Func<T,bool>> where,Expression<Func<T,TKey>> order,bool asc)
        {
            if(pageIndex < 1) pageIndex = 1;
            if(pageSize < 1) pageSize = 10;
            IQueryable<T> _list = DbContext.Set<T>().Where(where);
            _list = asc ? _list.OrderBy(order) : _list.OrderByDescending(order);
            totalNumber = _list.Count();
            return _list.Skip((pageIndex - 1) * pageIndex).Take(pageSize);
        }

        #endregion

        #endregion

        #region 增2

        /////// 一种方法                                               //////
        /////// DbContext.Set<T>().Add(entity);                        //////

        ////// 另一种方法                                               /////
        ////// DbContext.Set<T>().Attach(entity);                       //////
        ////// DbContext.Entry<T>(entity).State = EntityState.Added;    //////

        //////  return isSave ? DbContext.SaveChanges() : 0;            //////




        /// <summary>
        /// 添加实体-立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响的对象的数目</returns>
        public int Add(T entity)
        {
            return Add(entity,true);
        }

        /// <summary>
        /// 添加实体-选择是否立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Add(T entity,bool isSave)
        {
            //一种方法
            // DbContext.Set<T>().Add(entity);
            //另一种方法
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Added;

            return isSave ? DbContext.SaveChanges() : 0;
        }

        #endregion

        #region 更2

        /// <summary>
        /// 更新实体-立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Update(T entity)
        {
            return Update(entity,true);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Update(T entity,bool isSave)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Modified;
            return isSave ? DbContext.SaveChanges() : 0;
        }

        #endregion

        #region 删

        #region 删除实体-单-2重载
        /// <summary>
        /// 删除实体【立即保存】
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响的对象的数目</returns>
        public int Delete(T entity)
        {
            return Delete(entity,true);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Delete(T entity,bool isSave)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Deleted;
            return isSave ? DbContext.SaveChanges() : 0;

            //DbContext.Set<T>().Remove(entity);
            //return isSave ? DbContext.SaveChanges() : 0;
        }

        #endregion

        #region 删除实体-多-1重载


        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns>受影响的对象的数目</returns>
        public int Delete(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }

        #endregion



        #endregion

        /////////////////////////////////////


        #region 统计数量2（实体中记录数）

        /// <summary>
        /// 记录数
        /// </summary>
        /// <returns></returns>
        public int Count( )
        {
            return DbContext.Set<T>().Count();
        }


        /// <summary>
        /// 记录数  -拉姆表达式
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<T,bool>> predicate)
        {
            return DbContext.Set<T>().Count(predicate);
        }
        
        #endregion

        #region 是否存在

        /// <summary>
        /// 记录是否存在-拉姆表达式
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public bool IsContains(Expression<Func<T,bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        #endregion

        #region 保存到数据库

        /// <summary>
        /// 保存数据【在Add、Upate、Delete未立即保存的情况下使用】
        /// </summary>
        /// <returns>受影响的记录数</returns>
        public int Save( )
        {
            return DbContext.SaveChanges();
        }


        #endregion




        ///////////////////////////////////




    }
}
