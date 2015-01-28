using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospiceNiagara.Startup))]
namespace HospiceNiagara
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
