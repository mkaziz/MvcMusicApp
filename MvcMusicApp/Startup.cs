using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcMusicApp.Startup))]
namespace MvcMusicApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
