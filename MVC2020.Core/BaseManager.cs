using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Common;
using MVC2020.DataLibrary;


//向下封装一层  增加返回结果
//（如ADD  添加成功如否  和  添加的数据）

namespace MVC2020.Core
{
    /// <summary>
    /// 
    /// 使用虚方法接口  不可实例化  只可继承后实例化
    /// </summary>
    public abstract class BaseManager<T> where T : class
    {
        /// <summary>
        /// 数据仓储类
        /// </summary>
        public Repository<T> Repository;

        /// <summary>
        /// 默认构造函数==  程序内赋值调用  有参参数
        /// </summary>
        public BaseManager( )
            : this(ContextFactory.CurrentContext())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        public BaseManager(DbContext dbContext)
        {
            Repository = new Repository<T>(dbContext);
        }

        ////报错  无效参数
        ////Paging<User> pagingUser = new Paging<User>();
        //public bool FindPageList(int pageSize,int pageIndex,out int totalNumber,Expression<Func<T,bool>> where11,MVC2020.Core.GeneralTypes.OrderParam[] orderParams) 
        //{

        //    pagingUser.Items = Repository.FindPageList(pageSize,pageIndex,out totalNumber,where11,orderParams);
        //    return true;
 
        //}

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>成功时属性【Data】为添加后的数据实体</returns>
        public virtual Response Add(T entity)
        {
            Response _response = new Response();
            if(Repository.Add(entity)>0)
            {
                _response.Code = 1;
                _response.Message = "添加数据成功！";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "添加数据失败！";
            }

            return _response;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>成功时属性【Data】为更新后的数据实体</returns>
        public virtual Response Update(T entity)
        {
            Response _response = new Response();
            if (Repository.Update(entity) > 0)
            {
                _response.Code = 1;
                _response.Message = "更新数据成功！";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "更新数据失败！";
            }

            return _response;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>Code：0-删除失败；1-删除陈功；10-记录不存在</returns>
        public virtual Response Delete(int ID)
        {
            Response _response = new Response();
            var _entity = Find(ID);
            if (_entity == null)
            {
                _response.Code = 10;
                _response.Message = "记录不存在！";
            }
            else
            {
                if (Repository.Delete(_entity) > 0)
                {
                    _response.Code = 1;
                    _response.Message = "删除数据成功！";
                }
                else
                {
                    _response.Code = 0;
                    _response.Message = "删除数据失败！";
                }
            }
            

            return _response;
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>实体</returns>
        public virtual T Find(int ID)
        {
            return Repository.Find(ID);
        }

        /// <summary>
        /// 查找数据列表-【所有数据】
        /// </summary>
        /// <returns>所有数据</returns>
        public IQueryable<T> FindList()
        {
            return Repository.FindList();
        }

        /// <summary>
        /// 查找分页数据
        /// </summary>
        /// <param name="paging">分页数据</param>
        /// <returns>分页数据</returns>
        public Paging<T> FindPageList(Paging<T> paging)
        {
            paging.Items = Repository.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber).ToList();
            return paging;
        }


        ///// <summary>
        ///// 查找分页数据  paixu
        ///// </summary>
        ///// <param name="paging">分页数据</param>
        ///// <returns>分页数据</returns>
        //public IQueryable<T> FindPageList(Expression<Func<T,bool>> where)
        //{


        //    //IQueryable<T> dd1 = Repository.FindPageListcesi(where);
        //    //return dd1;


        //    OrderParam[]  ddd=  new OrderParam[1]; 
        //    OrderParam ork = new OrderParam();
        //    ork.PropertyName="UserID";
        //    ork.Method=OrderMethod.ASC;

        //    ddd[0] =ork;

        //    int aa = 3;
        //    IQueryable<T> dd = Repository.FindPageList(1,1,out aa,where,ddd);//Repository  此类没有继承仓促

        //    return  dd;
        //}

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns>总记录数</returns>
        public virtual int Count()
        {
            return Repository.Count();
        }




        ///// <summary>
        ///// cesi--List
        ///// </summary>
        ///// <param name="where"></param>
        ///// <returns></returns>
        //public IQueryable<T> cesi66(Expression<Func<T,bool>> where)
        //{
        //    return Repository.GetEntity(where,1,2,3,"message","UserID",true);
            
        //}
    

    }
}
