using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcleUrbanGardens.Domain
{
    public class Category
    {
        public virtual int Id { get; set; }

        // to enable sub-categories
        public virtual int? ParentId { get; set; }
        //public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }

        // a category contains many products
        public virtual ICollection<Product> Products { get; set; }

        // data
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public virtual string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Create Date")]
        public virtual DateTime CreateDate { get; set; }

        [Display(Name = "Updated By")]
        public virtual string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Update Date")]
        public virtual DateTime? UpdateDate { get; set; }

        [Required]
        [Display(Name = "Image")]
        public virtual string ImagePath { get; set; }

        public bool IsDeleted { get; set; }
    }
}



