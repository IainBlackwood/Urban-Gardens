using AcleUrbanGardens.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class IndexProductViewModelOld
    {
        [Required]
        [Display(Name = "Product Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public virtual string CategoryName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual int? CategoryId { get; set; }

        [Required]
        public virtual IQueryable<Product> Products { get; set;}

        [Required]
        public virtual IQueryable<Category> Categories { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}