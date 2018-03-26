using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(zderko.Startup))]
namespace zderko
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
