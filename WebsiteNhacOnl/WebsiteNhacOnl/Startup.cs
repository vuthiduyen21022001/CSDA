using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteNhacOnl.Startup))]
namespace WebsiteNhacOnl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
