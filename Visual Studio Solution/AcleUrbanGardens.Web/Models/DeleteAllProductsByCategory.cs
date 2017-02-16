using AcleUrbanGardens.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class DeleteAllProductsByCategory
    {
        public IQueryable<Product> products { get; set; }

        public int? CategoryId { get; set; }
    }
}