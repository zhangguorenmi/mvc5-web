using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2020.Core.GeneralFunction;

namespace MVC2020.Web.Areas.Member.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>

    public class HomeController : Controller
    {
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Index( )
        {
            return View();
        }

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index2( )
        {
            return View();
        }

    }
}