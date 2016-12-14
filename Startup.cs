using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HDILT.Startup))]
namespace HDILT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
