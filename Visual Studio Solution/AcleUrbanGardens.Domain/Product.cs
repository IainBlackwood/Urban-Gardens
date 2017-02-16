using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AcleUrbanGardens.Domain
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string LongDescription { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual string ProductImage { get; set; }

        public virtual int? CategoryId { get; set; }

        public bool FromCategoryDetail { get; set; }
        public bool IsDeleted { get; set; }

    }
}
