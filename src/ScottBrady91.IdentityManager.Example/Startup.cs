using IdentityManager;
using IdentityManager.AspNetIdentity;
using IdentityManager.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

namespace ScottBrady91.IdentityManager.Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityManagerServiceFactory();
            factory.IdentityManagerService = new Registration<IIdentityManagerService>(Create());

            app.UseIdentityManager(new IdentityManagerOptions {Factory = factory});
        }

        private IIdentityManagerService Create()
        {
            var context =
                new IdentityDbContext(
                    @"Data Source=.\SQLEXPRESS;Initial Catalog=AspIdentity;Integrated Security=true");

            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var managerService =
                new AspNetIdentityManagerService<IdentityUser, string, IdentityRole, string>(userManager, roleManager);

            return managerService;
        }
    }
}