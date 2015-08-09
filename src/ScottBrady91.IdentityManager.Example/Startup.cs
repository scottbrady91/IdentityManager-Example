namespace ScottBrady91.IdentityManager.Example
{
    using global::IdentityManager;
    using global::IdentityManager.AspNetIdentity;
    using global::IdentityManager.Configuration;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityManagerServiceFactory();

            factory.Register(new Registration<IdentityDbContext>());
            factory.Register(new Registration<UserStore<IdentityUser>>());
            factory.Register(new Registration<RoleStore<IdentityRole>>());
            factory.Register(new Registration<UserManager<IdentityUser,string>>(x => new UserManager<IdentityUser>(x.Resolve<UserStore<IdentityUser>>())));
            factory.Register(new Registration<RoleManager<IdentityRole,string>>(x => new RoleManager<IdentityRole>(x.Resolve<RoleStore<IdentityRole>>())));
            
            factory.IdentityManagerService =
                new Registration<IIdentityManagerService, AspNetIdentityManagerService<IdentityUser, string, IdentityRole, string>>();
            
            app.UseIdentityManager(new IdentityManagerOptions { Factory = factory });
        }
    }
}