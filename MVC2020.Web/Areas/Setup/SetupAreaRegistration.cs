using System.Web.Mvc;

namespace MVC2020.Web.Areas.Setup
{
    public class SetupAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Setup";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Setup_default",
                "Setup/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 new string[] { "MVC2020.Web.Areas.Setup.Controllers" } //后加
            );
        }
    }
}