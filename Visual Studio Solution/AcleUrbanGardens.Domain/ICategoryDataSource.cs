using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcleUrbanGardens.Domain
{
    public interface ICategoryDataSource
    {
        IQueryable<Category> Categories { get;}
        IQueryable<Product> Products { get; }

        void Save();
    }
}
