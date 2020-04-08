using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2020.Core.GeneralFunction;
using MVC2020.Core.Model;

namespace MVC2020.Web.Areas.Member.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [AdminAuthorize]
    public class RoleController : Controller
    {

        private RoleManager roleManager = new RoleManager();


        // GET: Member/Role
        public ActionResult Index( )
        {
            return View();
        }

        /// <summary>
        ///  列表【Json】
        /// </summary>
        /// <returns></returns>
        public JsonResult ListJson( )
        {
            return Json(roleManager.FindList());
        }


        /// <summary>
        ///  添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add( )
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Role role)
        {
            if(ModelState.IsValid)
            {
                if(roleManager.Add(role).Code == 1)
                {
                    return View("Mess",new MVC2020.Core.GeneralTypes.Mess()
                    {
                        Title = "添加角色成功",
                        Message = "你已成功添加了角色【" + role.Name + "】",
                        Buttons = new List<string>() { "<a href=\"" + Url.Action("Index","Role") + "\" class=\"btn btn-default\">角色管理</a>","<a href=\"" + Url.Action("Add","Role") + "\" class=\"btn btn-default\">继续添加</a>" }
                    });
                }
            }
            return View(role);
        }

        public ActionResult cesi( )
        {
            //Mess1 未找到视图“Mess1”或其母版视图，或没有视图引擎支持搜索的位置
            return View("Mess",new MVC2020.Core.GeneralTypes.Mess()
            {
                Title = "添加角色成功",
                Message = "你已成功添加了角色"
            });
        }



    }
}