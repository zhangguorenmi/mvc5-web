using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2020.Web.Areas.Setup.Controllers
{
    public class HomeController : Controller
    {
        // GET: Setup/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}