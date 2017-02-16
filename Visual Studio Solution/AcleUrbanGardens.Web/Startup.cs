using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcleUrbanGardens.Web.Startup))]
namespace AcleUrbanGardens.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
