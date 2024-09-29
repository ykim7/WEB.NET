using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YJK2237A4.Startup))]
namespace YJK2237A4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
