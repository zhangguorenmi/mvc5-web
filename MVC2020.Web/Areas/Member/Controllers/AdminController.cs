using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2020.Core;
using MVC2020.Core.GeneralFunction;
using MVC2020.Core.Model;
using MVC2020.Web.Areas.Member.Models;


namespace MVC2020.Web.Areas.Member.Controllers
{
    /// <summary>
    /// 管理员控制器
    /// </summary>
    [AdminAuthorize]
    public class AdminController : Controller
    {

        private AdministratorManager adminManager = new AdministratorManager();

        // GET: Member/Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login( )
        {
            return View();
        }

        /// <summary>
        /// 登录的处理方法HttpPost
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                string _passowrd = Security.Sha256(loginViewModel.Password);
                var _response = adminManager.Verify(loginViewModel.Accounts,_passowrd);
                if(_response.Code == 1)
                {
                    var _admin = adminManager.Find(loginViewModel.Accounts);
                    Session.Add("AdminID",_admin.AdministratorID);
                    Session.Add("Accounts",_admin.Accounts);
                    _admin.LoginTime = DateTime.Now;
                    _admin.LoginIP = Request.UserHostAddress;
                    adminManager.Update(_admin);
                    return RedirectToAction("Index","Home");
                }
                else if(_response.Code == 2) ModelState.AddModelError("Accounts",_response.Message);
                else if(_response.Code == 3) ModelState.AddModelError("Password",_response.Message);

                else ModelState.AddModelError("",_response.Message);//见：ModelState.AddModelError使用
            }
            return View(loginViewModel);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout( )
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


        /// <summary>
        /// 我的资料
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo( )
        {
            return View(adminManager.Find(Session["Accounts"].ToString()));
        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MyInfo(FormCollection form)
        {
            var _admin = adminManager.Find(Session["Accounts"].ToString());

            if(_admin.Password != form["Password"])
            {
                _admin.Password = Security.Sha256(form["Password"]);
                var _resp = adminManager.ChangePassword(_admin.AdministratorID,_admin.Password);
                if(_resp.Code == 1) ViewBag.Message = "<div class=\"alert alert-success\" role=\"alert\"><span class=\"glyphicon glyphicon-ok\"></span>修改密码成功！</div>";
                else ViewBag.Message = "<div class=\"alert alert-danger\" role=\"alert\"><span class=\"glyphicon glyphicon-remove\"></span>修改密码失败！</div>";
            }
            return View(_admin);
        }


        /// <summary>
        /// 提供管理员列表数据--无视图
        /// </summary>
        /// <returns></returns>
       
        public JsonResult ListJson( )
        {


            ////无法获取数量？？
            //IQueryable<Administrator> tt = adminManager.FindList();

            //int aa = tt.Count();//查看数组个数

            IQueryable<Administrator> tt = adminManager.FindList();

            return Json(tt);//adminManager.FindList()
        }


      


    }
}