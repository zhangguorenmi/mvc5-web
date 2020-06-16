using System.Web.Mvc;

namespace MVC2020.Web.Areas.sdhrjt
{
    public class sdhrjtAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "sdhrjt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "sdhrjt_default",
                "sdhrjt/{controller}/{action}/{id}",
                new { controller = "Default",action = "Index",id = UrlParameter.Optional },
                   new string[] { "MVC2020.Web.Areas.sdhrjt.Controllers" } //后加
            );
        }
    }
}