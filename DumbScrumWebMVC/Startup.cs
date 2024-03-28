using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DumbScrumWebMVC.Startup))]
namespace DumbScrumWebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
