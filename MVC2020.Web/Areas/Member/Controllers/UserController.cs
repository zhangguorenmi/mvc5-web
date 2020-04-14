using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //int?可以为空ActionResult JsonResult
        public ActionResult PageListJson(int? roleID,string username,string name,int? sex,string email,int? pageNumber,int? pageSize,int? order)
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




            Paging<User> pag6 = new Paging<User>();
            UserManager userManager22 = new UserManager();//添加变量

            pag6.PageIndex = 1;
            pag6.PageSize = 1;
            pag6.TotalNumber = 3;
            var _paging11 = userManager22.FindPageList(pag6);

            //ActionResult aa1 = Json(new { total = _paging11.TotalNumber,rows = _paging11.Items });

            ///////////////////////////////////////////////////////////////////////////////////////////

            //if(pageNumber != null && pageNumber > 0) paging.PageIndex = (int)pageNumber;//当前页
            //if(pageSize != null && pageSize > 0) paging.PageSize = (int)pageSize;//每页数
            //paging.TotalNumber = 3;



           //IQueryable<User> tt = userManager.FindPageList(u => u.UserID > 0);
           // var _paging11 = userManager.FindPageList(u => u.UserID > 0);

           ActionResult aa1 = Json(new { total = _paging11.TotalNumber,rows = _paging11.Items });
           return aa1;



        }


      
    }
}