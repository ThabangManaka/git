using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmallAddressBook.Startup))]
namespace SmallAddressBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
