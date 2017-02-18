namespace AcleUrbanGardens.Web.Migrations.ApplicationDbContext
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Run following in package manager console to enable migrations
    /// Enable-Migrations -ContextTypeName AcleUrbanGardens.Web.Models.ApplicationDbContext -MigrationsDirectory Migrations\ApplicationDbContext -Verbose
    /// Run following in package manager console to update-database
    /// Update-Database -ConfigurationTypeName AcleUrbanGardens.Web.Migrations.ApplicationDbContext.Configuration -Verbose
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<AcleUrbanGardens.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
        }

        protected override void Seed(AcleUrbanGardens.Web.Models.ApplicationDbContext context)
        {
            // allways create the roles
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            roleManager.Create(new IdentityRole { Name = "admin" });
            roleManager.Create(new IdentityRole { Name = "user" });

            // set up for creating default users
            var store = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(store);


            // create me
            if (!context.Users.Any(u => u.UserName == "iblackwoodhome@btconnect.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "iblackwoodhome@btconnect.com",
                    Email = "iblackwoodhome@btconnect.com",
                    EmailConfirmed = false,
                    PasswordHash = new PasswordHasher().HashPassword("Password01!")
                };

                userManager.Create(user);
                userManager.AddToRole(user.Id, "admin");

            }

            // create nick
            if (!context.Users.Any(u => u.UserName == "acle.urbangardens@hotmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "acle.urbangardens@hotmail.com",
                    Email = "acle.urbangardens@hotmail.com",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher().HashPassword("Password02!")
                };

                userManager.Create(user);
                userManager.AddToRole(user.Id, "admin");

            }

            // test user
            if (!context.Users.Any(u => u.UserName == "flakboy@hotmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "flakboy@hotmail.co.uk",
                    Email = "flakboy@hotmail.co.uk",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher().HashPassword("Password03!")
                };
                userManager.Create(user);
                //userManager.AddToRole(user.Id, "user");
            }

            // test user ii
            if (!context.Users.Any(u => u.UserName == "test_oauth@outlook.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "test_oauth@outlook.com",
                    Email = "test_oauth@outlook.com",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher().HashPassword("Password04!")
                };
                userManager.Create(user);
                //userManager.AddToRole(user.Id, "user");
            }
        }
    }
}