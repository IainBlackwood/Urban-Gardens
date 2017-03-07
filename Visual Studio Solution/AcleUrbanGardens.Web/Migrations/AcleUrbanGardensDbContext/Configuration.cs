namespace AcleUrbanGardens.Web.Migrations.AcleUrbanGardensDbContext
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;


    /// <summary>
    /// Run following in package manager console to enable migrations
    /// Enable-Migrations -ContextTypeName AcleUrbanGardens.Web.Infrastructure.AcleUrbanGardensDb -MigrationsDirectory Migrations\AcleUrbanGardensDbContext -Verbose
    /// Run following in package manager console to update-database
    /// Update-Database -ConfigurationTypeName AcleUrbanGardens.Web.Migrations.AcleUrbanGardensDbContext.Configuration -Verbose
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<AcleUrbanGardens.Web.Infrastructure.AcleUrbanGardensDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\AcleUrbanGardensDbContext";
        }

        protected override void Seed(AcleUrbanGardens.Web.Infrastructure.AcleUrbanGardensDb context)
        {
            // add the default categories and sub-categories
            context.Categories.AddOrUpdate(c => c.Name,
                new Category()
                {
                    Name = Constants.CATEGORY_UNASSIGNED_PRODUCTS,
                    Description = "DO NOT DELETE - This category is used to store any products that were attached to a Category that was deleted",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "unassigned-products.png"
                },
                new Category()
                {
                    Name = "Promotions",
                    Description = "Items in this category will be spotlighted on the home page",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "promotions.png"
                },
                new Category()
                {
                    Name = "Grow Kits",
                    Description = "Find a selection of tailored kits, from starter grows all the way up to expert systems.",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "grow-kits.png"
                },
                new Category()
                {
                    Name = "Grow Lights",
                    Description = "Find an excellent selection of HPS,Sodium LED and CFL lamps",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "grow-lights.png"
                },
                new Category()
                {
                    Name = "Lighting and Ballasts",
                    Description = "Find an excellent selection of HPS,Sodium LED and CFL lamps with ballasts to suit",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "lighting-ballasts.png"
                },
                new Category()
                {
                    Name = "Grow Tents",
                    Description = "A excellent selection of grow tents",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "grow-tents.png"
                },
                new Category()
                {
                    Name = "Air Control",
                    Description = "Fans filters and temperature controllers",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "air-control.png"
                },
                new Category()
                {
                    Name = "Grow Systems",
                    Description = "From wilma to rush systems we can supply them all",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "grow-systems.png"
                },
                new Category()
                {
                    Name = "Nutrients",
                    Description = "All brands of nutrients available",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "nutrients.png"
                },
                new Category()
                {
                    Name = "Growing Media",
                    Description = "Great quality growing media available",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "growing-media.png"
                },
                new Category()
                {
                    Name = "Propagation",
                    Description = "Get your crops of to a great early start with our propagation kits",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "propagation.png"
                }, new Category()
                {
                    Name = "Pest/Disease Control",
                    Description = "Find solutions to pest and disease problems.",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "pest-disease-control.png"
                },
                new Category()
                {
                    Name = "Tools and Accessories",
                    Description = "Get tools and accessories to help make your garden more vibrant",
                    CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17",
                    CreateDate = DateTime.UtcNow,
                    ImagePath = "tools-accessories.png"
                });

            // add the products
        }
    }
}
