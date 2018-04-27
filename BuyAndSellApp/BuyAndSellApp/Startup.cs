using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuyAndSelApp.Startup))]
namespace BuyAndSelApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
