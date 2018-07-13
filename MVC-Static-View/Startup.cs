using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Static_View.Startup))]
namespace MVC_Static_View
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
