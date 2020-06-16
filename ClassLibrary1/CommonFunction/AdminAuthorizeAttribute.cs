using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;    //手动引入  HttpContextBase
using System.Web.Mvc;//手动引入  AuthorizeAttribute


//AdminAuthorizeAttribute继承自AuthorizeAttribute，
//重写AuthorizeCore方法，通过Session["AdminID"]来判断管理员是否已经登录，
//重写HandleUnauthorizedRequest方法来处理未登录时的页面跳转。

namespace MVC2020.Common
{
    /// <summary>
    /// 管理员身份验证类
    /// </summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 重写自定义授权检查
        /// </summary>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.Session["AdminID"] == null) return false;
            else return true;
        }
        /// <summary>
        /// 重写未授权的 HTTP 请求处理
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //~/MVC2020.Web/Areas/Member/Views/Home/Index2
            filterContext.Result = new RedirectResult("~/Member/Admin/Login");


        }
    }
}
