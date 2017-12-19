using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindYourSportBuddy.UI.Startup))]
namespace FindYourSportBuddy.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
