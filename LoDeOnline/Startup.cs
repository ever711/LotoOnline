using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoDeOnline.Startup))]
namespace LoDeOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}