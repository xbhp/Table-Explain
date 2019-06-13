using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TableExplain_mvc.Startup))]
namespace TableExplain_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
