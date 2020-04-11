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
            Paging<Administrator> pag6 = new Paging<Administrator>();
            AdministratorManager userManager22 = new AdministratorManager();//添加变量

            //pag6.PageIndex = 1;
            //pag6.PageSize = 1;
            //pag6.TotalNumber = 3;
            //var _paging11 = userManager22.FindPageList(pag6);

            //ActionResult aa1 = Json(new { total = _paging11.TotalNumber,rows = _paging11.Items });

            ///////////////////////////////////////////////////////////////////////////////////////////

            if(pageNumber != null && pageNumber > 0) paging.PageIndex = (int)pageNumber;//当前页
            if(pageSize != null && pageSize > 0)     paging.PageSize = (int)pageSize;//每页数
            paging.TotalNumber = 3;

              

            var _paging = userManager.FindPageList(u => u.UserID>0);

           

            return Json(_paging);
        }


        public ActionResult PageListJson(int? roleID,string username,string name,string email,int? pageNumber,int? pageSize,int? order)
        {
            if(pageNumber != null && pageNumber > 0) paging.PageIndex = (int)pageNumber;//当前页
            if(pageSize != null && pageSize > 0) paging.PageSize = (int)pageSize;//每页数
            paging.TotalNumber = 3;
            var _paging = userManager.FindPageList(paging);
            return Json(new { total = _paging.TotalNumber,rows = _paging.Items });
        }
    }
}