using AcleUrbanGardens.Domain;
using System.Data.Entity;
using System.Linq;

namespace AcleUrbanGardens.Web.Infrastructure
{
    public class AcleUrbanGardensDb : DbContext, ICategoryDataSource
    {
        public AcleUrbanGardensDb() : base("UrbanGardenConnection")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        IQueryable<Category> ICategoryDataSource.Categories
        {
            get { return Categories; }
        }

        IQueryable<Product> ICategoryDataSource.Products
        {
            get { return Products; }
        }

        void ICategoryDataSource.Save()
        {
            SaveChanges();
        }
    }
}