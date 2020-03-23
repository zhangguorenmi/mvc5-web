using System.Web.Mvc;

namespace MVC2020.Web.Areas.Member
{
    /// <summary>
    /// 修改Member区域路由  2020/03/16
    /// </summary>
    public class MemberAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Member";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Member_default",
                "Member/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "MVC2020.Web.Areas.Member.Controllers" } //后加

            );
        }
    }
}