using Microsoft.Owin;
using Owin;
using MyWebApplication;
[assembly: OwinStartup(typeof(MyWebApplication.Startup))]
namespace MyWebApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}