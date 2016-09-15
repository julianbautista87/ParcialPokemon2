using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParcialPokemon.Startup))]
namespace ParcialPokemon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
