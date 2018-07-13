using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_StaticView.Startup))]
namespace MVC_StaticView
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
