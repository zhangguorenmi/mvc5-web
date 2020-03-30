using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2020.Core;
using MVC2020.Core.GeneralFunction;
using MVC2020.Core.Model;
using MVC2020.Web.Areas.Member.Models;
using System.Web.ModelBinding;
using MVC2020.Core.GeneralTypes;


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
        public ActionResult Index( )
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
        /// 我的资料 -  post 表单提交FormCollection
        /// </summary>
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




        /// <summary>
        /// 添加的【分部视图】
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddPartialView( )
        {
            return PartialView();
        }

        /// <summary>
        /// 添加【Json】
        /// </summary>
        /// <param name="addAdmin"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        //[ActionName("AddPartialView")]
        public JsonResult AddPartialView(AddAdminViewModel addAdmin)
        {
            MVC2020.Core.GeneralTypes.Response resp = new Core.GeneralTypes.Response();

            if(ModelState.IsValid)
            {
                if(adminManager.HasAccounts(addAdmin.Accounts))
                {
                    resp.Code = 0;
                    resp.Message = "帐号已存在";
                }
                else
                {
                    Administrator admin = new Administrator();
                    admin.Accounts = addAdmin.Accounts;
                    admin.CreateTime = System.DateTime.Now;
                    admin.Password = Security.Sha256(addAdmin.Password);
                    resp = adminManager.Add(admin);
                }
            }
            else
            {
                resp.Code = 0;
                resp.Message = GetModelErrorString.GetModelError(ModelState);
            }
            return Json(resp);
        }

        /// <summary>
        /// 删除 
        /// Response.Code:1-成功，2-部分删除，0-失败
        /// Response.Data:删除的数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteJson(List<int> ids)//【重要】【重要】【重要】ids名称为post中json的键值名称
        {
            int _total = ids.Count();
            Response _res = new Response();
            //不准删除当前管理员
            int _currentAdminID = int.Parse(Session["AdminID"].ToString());
            if(ids.Contains(_currentAdminID))
            {
                ids.Remove(_currentAdminID);
            }

            //批量删除
            _res = adminManager.Delete(ids);

            if(_res.Code == 1 && _res.Data < _total)
            {
                _res.Code = 2;
                _res.Message = "共提交删除" + _total + "名管理员,实际删除" + _res.Data + "名管理员。\n原因：不能删除当前登录的账号";
            }
            else if(_res.Code == 2)
            {
                _res.Message = "共提交删除" + _total + "名管理员,实际删除" + _res.Data + "名管理员。";
            }
            return Json(_res);
        }




        /// <summary>
        /// 测试 
        ///  { "data1": 11 }   =  List<int> data1 = int data1
        ///  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteJson11(int data1)//换成 new Array(); 不执行
        {
           // int _total = data1.Count();

            return Json(data1);
        }


        /// <summary>
        /// 重置密码【Ninesky】
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            string _password = "Ninesky";
            Response _resp = adminManager.ChangePassword(id,Security.Sha256(_password));
            if(_resp.Code == 1) _resp.Message = "密码重置为：" + _password;
            return Json(_resp);
        }


        

    }
}