using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LinqKit;
using MVC2020.Core;
using MVC2020.Core.GeneralFunction;
using MVC2020.Core.GeneralTypes;
using MVC2020.Core.Model;

namespace MVC2020.Web.Areas.Member.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : Controller
    {
        private UserManager userManager = new UserManager();//添加变量

        Paging<User> paging = new Paging<User>();




        [AdminAuthorize]
        // GET: Member/User
        public ActionResult Index( )
        {
            return View();
        }



        /// <summary>
        /// 分页列表【json】
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="username">用户名</param>
        /// <param name="name">名称</param>
        /// <param name="sex">性别</param>
        /// <param name="email">Email</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="order">排序</param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult PageListJson(int? roleID,string username,string name,int? sex,string email,int? pageNumber,int? pageSize,int? order)
        {
            Paging<User> _pagingUser = new Paging<User>();
            if(pageNumber != null && pageNumber > 0) _pagingUser.PageIndex = (int)pageNumber;
            if(pageSize != null && pageSize > 0) _pagingUser.PageSize = (int)pageSize;
            var _paging = userManager.FindPageList(_pagingUser,roleID,username,name,sex,email,null);


            //return Json(_paging.Items);
            return Json(new { total = _paging.TotalNumber,rows = _paging.Items });
            //return Json(_paging.Items);
        }




        [HttpPost]
        //int?可以为空ActionResult JsonResult
        //int? roleID,string username,string name,int? sex,string email,int? pageNumber,int? pageSize,int? order
        public JsonResult cesiPageListJson( )
        {

            //裸测通过
            //1.类没问题
            //2.前段接收没问题
            //3.返回数据没问题
            //User perA = new User() { Name = "李四",Username = "男22" };
            //User perB = new User() { Name = "张三",Username = "女22" };
            //List<User> list = new List<User>();
            //list.Add(perA);
            //list.Add(perB);
            //JsonResult jsonResult = Json(list);
            //return jsonResult;




            ////Paging<User> pag6 = new Paging<User>();
            ////UserManager userManager22 = new UserManager();//添加变量

            //// pag6.PageIndex = 1;
            //// pag6.PageSize = 1;
            //// pag6.TotalNumber = 3;
            //// var _paging11 = userManager22.FindPageList(pag6);

            //// //ActionResult aa1 = Json(new { total = _paging11.TotalNumber,rows = _paging11.Items });

            //// ///////////////////////////////////////////////////////////////////////////////////////////

            //// //if(pageNumber != null && pageNumber > 0) paging.PageIndex = (int)pageNumber;//当前页
            //// //if(pageSize != null && pageSize > 0) paging.PageSize = (int)pageSize;//每页数
            //// //paging.TotalNumber = 3;



            //////IQueryable<User> tt = userManager.FindPageList(u => u.UserID > 0);
           
            ////// var _paging11 = userManager.FindPageList(u => u.UserID > 0);

            ////ActionResult aa1 = Json(new { total = _paging11.TotalNumber,rows = _paging11.Items });
            ////return aa1;

            //001
            //==================================================================================================

                ////20200423 测试成功  用ToList();   立即查询

                //UserManager userManager22 = new UserManager();//添加变量
                ////List<User> uis = userManager22.cesi66(u => u.UserID > 0);
                //IQueryable<User> uis = userManager22.cesi66(u => u.UserID > 0);
                ////int aa = uis.Count();//立即查询  不要延迟查询  失败

                //List<User> uis1 = uis.ToList(); 
            
                //int aa = uis.Count();

                ////ActionResult aa111 = Json(uis);
                //return Json(uis1);

            //====================================================================================================


            ///002
            //======================================================================================================

                ////2020-04-23 23:00   仓储中的FindPageList测试成功  查出来的是SQL语句  这里需要转换一下就可以执行语句 获得结果了
                //IQueryable<User> tt = userManager.FindPageList(u => u.UserID > 0);
                //List<User> uis1 = tt.ToList();
                //return Json(uis1);

            //===============================================================================================================



            return Json(null);
        }



    }
}