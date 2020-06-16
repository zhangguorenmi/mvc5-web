using System.Web.Mvc;

namespace MVC2020.Web.Areas.sdhrjtSys
{
    public class sdhrjtSysAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "sdhrjtSys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "sdhrjtSys_default",
                "sdhrjtSys/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}