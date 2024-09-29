using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YJK2237A5.Startup))]

namespace YJK2237A5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
