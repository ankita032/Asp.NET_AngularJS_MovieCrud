using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularMVC.Startup))]
namespace AngularMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
