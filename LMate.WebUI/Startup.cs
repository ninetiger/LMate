using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMate.WebUI.Startup))]
namespace LMate.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
