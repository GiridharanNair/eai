using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EAI_Sample_Project.Startup))]
namespace EAI_Sample_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
