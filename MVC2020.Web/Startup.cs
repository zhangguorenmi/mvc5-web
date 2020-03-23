using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC2020.Web.Startup))]
namespace MVC2020.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
