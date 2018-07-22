using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogSpace.Startup))]
namespace BlogSpace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
