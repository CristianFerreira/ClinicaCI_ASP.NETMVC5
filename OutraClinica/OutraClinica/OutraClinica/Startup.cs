using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OutraClinica.Startup))]
namespace OutraClinica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
