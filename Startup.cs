using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Application_Higher_Institution.Startup))]
namespace Web_Application_Higher_Institution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
