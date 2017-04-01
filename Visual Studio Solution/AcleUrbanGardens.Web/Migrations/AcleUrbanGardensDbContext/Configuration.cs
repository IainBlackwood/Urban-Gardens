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
                    ImagePath = "unassigned-products.png",
                    ParentId = null
                },

                new Category() { Name = "Grow Kits", Description = "Grow Kits: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "grow-kits.png" },
                new Category() { Name = "Grow Tents", Description = "Grow Tents: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "grow-tents.png" },
                new Category() { Name = "Growing Media", Description = "Growing Media: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "growing-media.png" },
                new Category() { Name = "Hydroponics Systems", Description = "Hydroponics Systems: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "hydroponics-systems.png" },
                new Category() { Name = "Lighting", Description = "Lighting: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "lighting.png" },
                new Category() { Name = "Nutrients", Description = "Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "nutrients.png" },
                new Category() { Name = "Pest and Disease Control", Description = "Pest and Disease Control: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "pest-and-disease-control.png" },
                new Category() { Name = "Pots and Trays", Description = "Pots and Trays: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "pots-and-trays.png" },
                new Category() { Name = "Promotions", Description = "Promotions: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "promotions.png" },
                new Category() { Name = "Propagation", Description = "Propagation: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "propagation.png" },
                new Category() { Name = "Tools and Accessories", Description = "Tools and Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "tools-and-accessories.png" },
                new Category() { Name = "Air Filters", Description = "Air Filters: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "air-filters.png", ParentId = 1 },
                new Category() { Name = "Air Humidity Control and Accessories", Description = "Air Humidity Control and Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "air-humidity-control-and-accessories.png", ParentId = 1 },
                new Category() { Name = "CO2 Kits and Accessories", Description = "CO2 Kits and Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "co2-kits-and-accessories.png", ParentId = 1 },
                new Category() { Name = "Ducting and Accessories", Description = "Ducting and Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ducting-and-accessories.png", ParentId = 1 },
                new Category() { Name = "Fan Controllers", Description = "Fan Controllers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "fan-controllers.png", ParentId = 1 },
                new Category() { Name = "Fans", Description = "Fans: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "fans.png", ParentId = 1 },
                new Category() { Name = "Ozone Generators", Description = "Ozone Generators: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ozone-generators.png", ParentId = 1 },
                new Category() { Name = "Temperature Controllers", Description = "Temperature Controllers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "temperature-controllers.png", ParentId = 1 },
                new Category() { Name = "Advanced Kits", Description = "Advanced Kits: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "advanced-kits.png", ParentId = 2 },
                new Category() { Name = "Beginner Kits", Description = "Beginner Kits: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "beginner-kits.png", ParentId = 2 },
                new Category() { Name = "Intermediate Kits", Description = "Intermediate Kits: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "intermediate-kits.png", ParentId = 2 },
                new Category() { Name = "Propagation Tents", Description = "Propagation Tents: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "propagation-tents.png", ParentId = 3 },
                new Category() { Name = "Specialist Tents", Description = "Specialist Tents: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "specialist-tents.png", ParentId = 3 },
                new Category() { Name = "Standard Tents", Description = "Standard Tents: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "standard-tents.png", ParentId = 3 },
                new Category() { Name = "Soil-Media", Description = "Soil-Media: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "soil-media.png", ParentId = 4 },
                new Category() { Name = "Coco-Media", Description = "Coco-Media: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "coco-media.png", ParentId = 4 },
                new Category() { Name = "Hydro-Media", Description = "Hydro-Media: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "hydro-media.png", ParentId = 4 },
                new Category() { Name = "Deep Water Culture", Description = "Deep Water Culture: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "deep-water-culture.png", ParentId = 5 },
                new Category() { Name = "Nutrient Film Technique", Description = "Nutrient Film Technique: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "nutrient-film-technique.png", ParentId = 5 },
                new Category() { Name = "Flood and Drain", Description = "Flood and Drain: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "flood-and-drain.png", ParentId = 5 },
                new Category() { Name = "Dripper", Description = "Dripper: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "dripper.png", ParentId = 5 },
                new Category() { Name = "Lamps", Description = "Lamps: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "lamps.png", ParentId = 6 },
                new Category() { Name = "Ballasts", Description = "Ballasts: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ballasts.png", ParentId = 6 },
                new Category() { Name = "Contactators and Timers", Description = "Contactators and Timers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "contactators-and-timers.png", ParentId = 6 },
                new Category() { Name = "Reflectors", Description = "Reflectors: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "reflectors.png", ParentId = 6 },
                new Category() { Name = "Addatives", Description = "Addatives: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "addatives.png", ParentId = 7 },
                new Category() { Name = "Soil-Nutrients", Description = "Soil-Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "soil-nutrients.png", ParentId = 7 },
                new Category() { Name = "Coco-Nutrients", Description = "Coco-Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "coco-nutrients.png", ParentId = 7 },
                new Category() { Name = "Hydro-Nutrients", Description = "Hydro-Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "hydro-nutrients.png", ParentId = 7 },
                new Category() { Name = "Organic-Nutrients", Description = "Organic-Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "organic-nutrients.png", ParentId = 7 },
                new Category() { Name = "Nutrient Management", Description = "Nutrient Management: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "nutrient-management.png", ParentId = 7 },
                new Category() { Name = "Pest Control", Description = "Pest Control: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "pest-control.png", ParentId = 8 },
                new Category() { Name = "Disease Control", Description = "Disease Control: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "disease-control.png", ParentId = 8 },
                new Category() { Name = "Pots", Description = "Pots: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "pots.png", ParentId = 9 },
                new Category() { Name = "Trays", Description = "Trays: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "trays.png", ParentId = 9 },
                new Category() { Name = "Seasonal Promotions", Description = "Seasonal Promotions: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "seasonal-promotions.png", ParentId = 10 },
                new Category() { Name = "Special Offers", Description = "Special Offers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "special-offers.png", ParentId = 10 },
                new Category() { Name = "Propagation Tents", Description = "Propagation Tents: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "propagation-tents.png", ParentId = 11 },
                new Category() { Name = "Propagation Lighting", Description = "Propagation Lighting: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "propagation-lighting.png", ParentId = 11 },
                new Category() { Name = "Propagation Nutrients", Description = "Propagation Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "propagation-nutrients.png", ParentId = 11 },
                new Category() { Name = "Tools", Description = "Tools: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "tools.png", ParentId = 12 },
                new Category() { Name = "Accessories", Description = "Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "accessories.png", ParentId = 12 },
                new Category() { Name = "Humidifiers", Description = "Humidifiers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "humidifiers.png", ParentId = 14 },
                new Category() { Name = "De-humidifiers", Description = "De-humidifiers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "de-humidifiers.png", ParentId = 14 },
                new Category() { Name = "Humidity Accessories", Description = "Humidity Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "humidity-accessories.png", ParentId = 14 },
                new Category() { Name = "CO2 Bags", Description = "CO2 Bags: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "co2-bags.png", ParentId = 15 },
                new Category() { Name = "CO2 Dosers", Description = "CO2 Dosers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "co2-dosers.png", ParentId = 15 },
                new Category() { Name = "CO2 Monitors", Description = "CO2 Monitors: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "co2-monitors.png", ParentId = 15 },
                new Category() { Name = "CO2 Accessories", Description = "CO2 Accessories: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "co2-accessories.png", ParentId = 15 },
                new Category() { Name = "Accoustic Ducting", Description = "Accoustic Ducting: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "accoustic-ducting.png", ParentId = 16 },
                new Category() { Name = "Combi Ducting", Description = "Combi Ducting: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "combi-ducting.png", ParentId = 16 },
                new Category() { Name = "Ducting Fittings and Clamps", Description = "Ducting Fittings and Clamps: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ducting-fittings-and-clamps.png", ParentId = 16 },
                new Category() { Name = "Silver Ducting", Description = "Silver Ducting: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "silver-ducting.png", ParentId = 16 },
                new Category() { Name = "Inline", Description = "Inline: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "inline.png", ParentId = 18 },
                new Category() { Name = "Silent", Description = "Silent: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "silent.png", ParentId = 18 },
                new Category() { Name = "Oscilating Fans", Description = "Oscilating Fans: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "oscilating-fans.png", ParentId = 18 },
                new Category() { Name = "Ceramic Metal Halide (CDM)", Description = "Ceramic Metal Halide (CDM): Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ceramic-metal-halide-(cdm).png", ParentId = 34 },
                new Category() { Name = "Compact Fluorescent Lamp (CFL)", Description = "Compact Fluorescent Lamp (CFL): Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "compact-fluorescent-lamp-(cfl).png", ParentId = 34 },
                new Category() { Name = "High Pressure Sodium (HPS)", Description = "High Pressure Sodium (HPS): Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "high-pressure-sodium-(hps).png", ParentId = 34 },
                new Category() { Name = "Light Emmiting Diode (LED)", Description = "Light Emmiting Diode (LED): Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "light-emmiting-diode-(led).png", ParentId = 34 },
                new Category() { Name = "Metal Halide (MH)", Description = "Metal Halide (MH): Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "metal-halide-(mh).png", ParentId = 34 },
                new Category() { Name = "Flourescent T-5 (T-5)", Description = "Flourescent T-5 (T-5): Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "flourescent-t-5-(t-5).png", ParentId = 34 },
                new Category() { Name = "400 volt HPS", Description = "400 volt HPS: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "400-volt-hps.png", ParentId = 34 },
                new Category() { Name = "Digital Ballasts", Description = "Digital Ballasts: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "digital-ballasts.png", ParentId = 35 },
                new Category() { Name = "Magnetic Ballasts", Description = "Magnetic Ballasts: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "magnetic-ballasts.png", ParentId = 35 },
                new Category() { Name = "400 volt Ballasts", Description = "400 volt Ballasts: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "400-volt-ballasts.png", ParentId = 35 },
                new Category() { Name = "Contactors", Description = "Contactors: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "contactors.png", ParentId = 36 },
                new Category() { Name = "Timers", Description = "Timers: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "timers.png", ParentId = 36 },
                new Category() { Name = "Air Cooled", Description = "Air Cooled: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "air-cooled.png", ParentId = 37 },
                new Category() { Name = "Parabolic", Description = "Parabolic: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "parabolic.png", ParentId = 37 },
                new Category() { Name = "Double Ended", Description = "Double Ended: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "double-ended.png", ParentId = 37 },
                new Category() { Name = "Root Addatives", Description = "Root Addatives: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "root-addatives.png", ParentId = 38 },
                new Category() { Name = "Bloom Addatives", Description = "Bloom Addatives: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "bloom-addatives.png", ParentId = 38 },
                new Category() { Name = "Veg Addatives", Description = "Veg Addatives: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "veg-addatives.png", ParentId = 38 },
                new Category() { Name = "Enzymes", Description = "Enzymes: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "enzymes.png", ParentId = 38 },
                new Category() { Name = "Benificial Bactieria", Description = "Benificial Bactieria: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "benificial-bactieria.png", ParentId = 38 },
                new Category() { Name = "Folia Sprays", Description = "Folia Sprays: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "folia-sprays.png", ParentId = 38 },
                new Category() { Name = "Vegative Soil Nutrients", Description = "Vegative Soil Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "vegative-soil-nutrients.png", ParentId = 39 },
                new Category() { Name = "Bloom Soil Nutrients", Description = "Bloom Soil Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "bloom-soil-nutrients.png", ParentId = 39 },
                new Category() { Name = "Vegative Coco Nutrients", Description = "Vegative Coco Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "vegative-coco-nutrients.png", ParentId = 40 },
                new Category() { Name = "Bloom Coco Nutrients", Description = "Bloom Coco Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "bloom-coco-nutrients.png", ParentId = 40 },
                new Category() { Name = "Vegative Hydro Nutrients", Description = "Vegative Hydro Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "vegative-hydro-nutrients.png", ParentId = 41 },
                new Category() { Name = "Bloom Hydro Nutrients", Description = "Bloom Hydro Nutrients: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "bloom-hydro-nutrients.png", ParentId = 41 },
                new Category() { Name = "pH Probes and Sensors", Description = "pH Probes and Sensors: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ph-probes-and-sensors.png", ParentId = 43 },
                new Category() { Name = "EC Probes and Sensors", Description = "EC Probes and Sensors: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ec-probes-and-sensors.png", ParentId = 43 },
                new Category() { Name = "Calibration Fluids", Description = "Calibration Fluids: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "calibration-fluids.png", ParentId = 43 },
                new Category() { Name = "pH adjustments", Description = "pH adjustments: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "ph-adjustments.png", ParentId = 43 },
                new Category() { Name = "Air Pots", Description = "Air Pots: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "air-pots.png", ParentId = 46 },
                new Category() { Name = "Square Pots", Description = "Square Pots: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "square-pots.png", ParentId = 46 },
                new Category() { Name = "Round Pots", Description = "Round Pots: Description", CreatedBy = "815f3858-56e0-4091-a1af-c23ad46fad17", CreateDate = DateTime.UtcNow, ImagePath = "round-pots.png", ParentId = 46 }
            );
        }
    }
}
